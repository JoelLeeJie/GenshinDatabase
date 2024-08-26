# GenshinDB
Showcase: https://youtu.be/OqJ2aEUyNsA

A database WPF app meant to store and display character, weapon, artifact info from Genshin Impact in a compact format.

Created using PostgreSQL and .Net WPF in C#.

Data sourced from: https://genshin-impact.fandom.com/wiki/Genshin_Impact_Wiki
Database hosted on Neon.Tech

More Projects at
- https://github.com/JoelLeeJie/Personal-Showcase
- https://www.youtube.com/playlist?list=PL3v4AEQxsf2Oesvkm0B3S9gZYKGmHnUMD

Download GenshinDB as a Zip   
https://downgit.github.io/#/home?url=https://github.com/JoelLeeJie/GenshinDatabase/tree/download/GenshinDB_Download  
or https://download-directory.github.io/?url=https://github.com/JoelLeeJie/GenshinDatabase/tree/download/GenshinDB_Download
Extract, and run GenshinDB-WPF.exe or setup.exe.
Alternatively, go to https://github.com/JoelLeeJie/GenshinDatabase/tree/download and download the zip file there.

Process:  
GenshinDB-RawData: Gather raw data from websites and convert into csv files, to copy to the genshindb database.  
CSV files written to system by RawData program are then transferred to database via terminal commands, using GenshinDB-SQL folder as reference.  
GenshinDB-WPF: A UI for accessing the database.  


