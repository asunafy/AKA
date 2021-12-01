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
  global  vlist
  for j in range(len(label)):
    while label[j]in vlist:
      label[j]+="0"
    str0 += label[j]+'.'
  str0 = str0[:-1]+'{'  
  for j in range(len(label)):
    #if ((label[j] == 'b')&(j!=0))|(label[j] == 'd')|(label[j] == 'x')|(label[j] == 'y'):
      #str0 += 'ht_'+temp[1]+'.Add('+label[j]+'_value,'+label[j]+');'
    #elif label[j] == 'pij':
    #  str0 += 'ht_'+temp[1]+'.Add(Pij_value,'+label[j]+');'
    #elif label[j] == 'ts_c':
    variable = label[j].replace("0","")    
    if variable == 'ts_c':
      str0 += 'ht_'+temp[1]+'.Add(TS_c_value,'+label[j]+');' 
    elif variable == 'encpass':
      str0 += 'ht_'+temp[1]+'.Add(EncPass_value,'+label[j]+');'  
    elif variable == 'userinfor':
      str0 += 'ht_'+temp[1]+'.Add(UserInfor_value,'+label[j]+');' 
    elif variable == 'hash_x':
      str0 += 'ht_'+temp[1]+'.Add(Hash_x_value,'+label[j]+');'
    elif variable == 'serinfor':
      str0 += 'ht_'+temp[1]+'.Add(Serinfor_value,'+label[j]+');'
    elif variable == 'veru':
      str0 += 'ht_'+temp[1]+'.Add(Veru_value,'+label[j]+');'
    elif variable == 'vers':
      str0 += 'ht_'+temp[1]+'.Add(Vers_value,'+label[j]+');'
    else:
      str0 += 'ht_'+temp[1]+'.Add('+variable.upper()+'_value,'+label[j]+');'
  str0 = str0[:-1]+';}'
  vlist += element.split("?")[1].split(".")
  vlist = list(set(vlist))
  return str0 
def BuildCompute(element):
  temp = element.split("(")[0].split("_")
  temp[-1] = temp[-1].split("(")[0]
  label = re.findall(r'[(](.*?)[)]', element)[0].split(",")
  entity = temp[-1]
  value = ''
  for k in range(1,len(temp)-1):
   value += temp[k]+'_'
  value = value[:-1]
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
  tmp = y.split("()")[0]+'()'
  print('CSP-like process of '+tmp+' before conversion:')  
  print(x)
  print('PAT process of '+tmp+' after conversion:')
  print(y)
def BuildStoreTable(element):
  temp = element.split("(")[0].split("_")
  label = re.findall(r'[(](.*?)[)]', element)[0].split(",")
  entity = temp[-1]
  str0 = temp[0]+'{'+entity+'.Add('+label[0]+'_value);}'
  return str0
def BuildCheckUID(element):
  element = "CheckUID_VerifierTable_C(UID)"
  temp = element.split("(")[0].split("_")
  temp[-1] = temp[-1].split("(")[0]
  label = re.findall(r'[(](.*?)[)]', element)[0].split(",")
  str0=temp[0]+'{if(!'+temp[1]+'.Contains('+'ht_'+temp[-1]+'.GetValue('+label[0]+'_value))){unregistered=true;}}'
  return str0

with open("phases_Bae.txt", "r") as f:
    data = f.readlines()
    print(data)

Protocol = ''
for test in data:
  test = test.replace(' ', '')
  Process = test.split("()=")[0]+"() = "
  actions = test.split("()=")[1].split("->")
  vlist =[]
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
    elif 'StoreTable' in element:
      str0 = BuildStoreTable(element)
    elif 'CheckUID' in element:
      str0 = BuildCheckUID(element)
    elif '_' in element:
      str0 = BuildAssignment(element)
    else:
      continue
    Process += str0 + ' -> '
  Process += 'Skip;'
  printConvert(test,Process)
  print('\n')
  Protocol += Process+'\n'

with open("preparation_Bae.csp", "r") as f:
    pre = f.read()

with open("combination_Bae.csp", "r") as f:
    com = f.read()

Protocol = pre+'\n'+Protocol+'\n'+com

with open("Out_Bae.csp","w") as f:
        f.write(Protocol) 




