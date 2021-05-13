#ifndef CAT_H
#define CAT_H

#define CAT_DLL

#ifdef CAT_DLL
#define CAT_API __declspec(dllexport)			
#else
#define CAT_API __declspec(dllimport)			
#endif 


#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

int CAT_API RAND(int max, int min, int n);

#ifdef __cplusplus
}
#endif // __cplusplus

#endif