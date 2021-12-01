import re
MAX = 20
MIN = 0
RandomCount = 0

def BuildAssignment(element):
  temp = element.split("_")
  temp[-1] = temp[-1].split("(")[0]
  label = re.findall(r'[(](.*?)[)]', element)
  str0 = temp[0]+'_'+temp[1]+"{"
  for j in range(2,len(temp)):
    str0 += 'ht_'+temp[j]+'.Add('+temp[1]+'_value,'+label[0]+');'
  str0 += '}'
  return str0
def BuildRAND(element):
  temp = element.split("_")
  temp[-1] = temp[-1].split("(")[0]
  label = re.findall(r'[(](.*?)[)]', element)
  str0 = temp[0]+'_'+temp[1]+"{" 
  global  RandomCount
  str0 += 'ht_'+temp[2]+'.Add('+temp[1]+'_value,call('+label[0]+','+str(MAX)+','+str(MIN)+','+str(RandomCount)+'));}'
  RandomCount += 1
  return str0  
def BuildSend(element):
  temp = element.split("!")[0].split("_")
  label = element.split("!")[1].split(".")
  str0 = temp[0]+'!'
  for j in range(len(label)):
    str0 += 'ht_'+temp[1]+'.GetValue('+label[j]+'_value).'
    if label[j]=='TS':
      str0 += 'ht_'+temp[1]+'.GetValue('+label[j]+'_c_value).'
  str0 = str0[:-1]
  return str0  
def BuildReceive(element):
  temp = element.split("?")[0].split("_")
  label = element.split("?")[1].split(".")
  str0 = temp[0]+'?'
  for j in range(len(label)):
    str0 += label[j]+'.'
  str0 = str0[:-1]+'{'  
  for j in range(len(label)):
    if ((label[j] == 'b')&(j!=0))|(label[j] == 'd')|(label[j] == 'x')|(label[j] == 'y'):
      str0 += 'ht_'+temp[1]+'.Add('+label[j]+'_value,'+label[j]+');'
    elif label[j] == 'pij':
      str0 += 'ht_'+temp[1]+'.Add(Pij_value,'+label[j]+');'
    elif label[j] == 'ts_c':
      str0 += 'ht_'+temp[1]+'.Add(TS_c_value,'+label[j]+');' 
    else:
      str0 += 'ht_'+temp[1]+'.Add('+label[j].upper()+'_value,'+label[j]+');'
  str0 = str0[:-1]+';}'
  return str0 
def BuildCompute(element):
  temp = element.split("(")[0].split("_")
  temp[-1] = temp[-1].split("(")[0]
  label = re.findall(r'[(](.*?)[)]', element)[0].split(",")
  if len(temp[2])==1:
    entity = temp[2]
    value = temp[1]
  else:
    entity = temp[4]
    value = temp[1]+'_'+temp[2]+'_'+temp[3]
  str0 = temp[0]+'_'+value+"{" 
  str0 += 'ht_'+entity+'.Add('+value+'_value,call('+label[0]
  for j in range(1,len(label)):
    if label[j]=='TS':
      str0 += ',ht_'+entity+'.GetValue('+label[j]+'_c_value)'
    else:
      str0 += ',ht_'+entity+'.GetValue('+label[j]+'_value)'
  if value=='SK':
    str0 += '));'+entity+'_SK=ht_'+entity+'.GetValue('+value+'_value);}'
  else:
    str0 += '));}'
  return str0
def BuildTimestamp(element):
  temp = element.split("_")
  temp[-1] = temp[-1].split("(")[0]
  label = re.findall(r'[(](.*?)[)]', element)[0].split(";")
  global  RandomCount
  str0 = temp[0]+'_'+temp[1]+"{" 
  str0 += 'ht_'+temp[2]+'.Add('+temp[1]+'_value,call('+label[0]+'));'+'ht_'+temp[2]+'.Add('+temp[1]+'_c_value,call('+label[1]+','+str(MAX)+','+str(MIN)+','+str(RandomCount)+'));}'
  RandomCount += 1
  return str0  
def BuildComputeCheck(element):
  temp = element.split("(")[0].split("_")
  temp[-1] = temp[-1].split("(")[0]
  label = re.findall(r'[(](.*?)[)]', element)[0].split(",") 
  entity = temp[2]
  value = temp[1]
  str0 = temp[0]+'_'+value+"{"+'if(call('+label[0]
  for j in range(1,len(label)):
    if label[j]=='TS':
      str0 += ',ht_'+entity+'.GetValue('+label[j]+'_c_value)'
    else:
      str0 += ',ht_'+entity+'.GetValue('+label[j]+'_value)'
  str0 += ')==ht_'+entity+'.GetValue('+value+'_value)){cs_check_'+value+'=true;}}'
  return str0
def BuildCheckDelay(element):
  temp = element.split("_")
  temp[-1] = temp[-1].split("(")[0]
  label = re.findall(r'[(](.*)[)]', element)[0].split(",")#贪婪匹配
  str0 = 'CheckDelay{if(call('+label[0]+',ht_'+temp[1]+'.GetValue('+label[1]+'_value),'+'call'+label[2]+',ht_'+temp[1]+'.GetValue('+label[3]+'_value)'+',ht_'+temp[1]+'.GetValue('+label[4]+'_value))==true){timeout=true;}}'
  return str0  
def printConvert(x,y):
  print('Before Convert:')  
  print(x)
  print('After Convert:')
  print(y)

with open("phases_Xue.txt", "r") as f:
    data = f.readlines()
    print(data)

Protocol = ''
for test in data:
  test = test.replace(' ', '')
  Process = test.split("()=")[0]+"() = "
  actions = test.split("()=")[1].split("->")
  for i in range(0,len(actions)):
    element = actions[i]
    if 'CheckDelay' in element:
      str0 = BuildCheckDelay(element)    
    elif ('RAND' in element)&(';RAND' not in element):
      str0 = BuildRAND(element)
    elif 'Timestamp' in element:
      str0 = BuildTimestamp(element)
    elif '!' in element:
      str0 = BuildSend(element) 
    elif '?' in element:
      str0 = BuildReceive(element) 
    elif 'ComputeCheck' in element:
      str0 = BuildComputeCheck(element)
    elif 'Compute' in element:
      str0 = BuildCompute(element)
    elif '_' in element:
      str0 = BuildAssignment(element)
    else:
      continue
    Process += str0 + ' -> '
  Process += 'Skip;'
  Protocol += Process+'\n'
print(Protocol)

with open("preparation_Xue.txt", "r") as f:
    pre = f.read()
with open("combination_Xue.txt", "r") as f:
    com = f.read()

Protocol = pre+'\n'+Protocol+'\n'+com

with open("Out_Xue.txt","w") as f:
        f.write(Protocol) 
