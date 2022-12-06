# GenshinDB
A database meant to store character, weapon, artifact and material info from Genshin Impact.  
May include a wpf for UI in the future to access this info.  

Info from https://api.genshin.dev/ and Shiori.  

(done)Step 1: Get data in text form from api.genshin.dev for all characters, weapons, artifacts.   
(error)Step 2: Store data in respective files; one file per character etc. Sort all files into respective folders(Character, Weapon, Artifact).  
Step 3: Read data from raw data files. Sort out necessary information and store in structs(CharacterStruct to store character information like names).  
                 Store structs in respective arrays.  
Step 4: From each list of structs, write line-by-line a csv file for each table.  
Step 5: Copy csv files to tables.  

Step 6: Create wpf to get data from tables.  
Step 7: wpf should be able to insert characterBuild data(data from user on which character + which weapon + which artifacts etc) into characterbuild table.  



GenshinDB - GenshinDB-RawData - GetRawData: GetRawDataProgram.cs takes data from https://api.genshin.dev/, and stores the data temporarily in RawData folder.   
It then converts the information and stores it in multiple csv files, one for each table, for copy.  
