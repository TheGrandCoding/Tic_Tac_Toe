#### This script copies all package DLLs into the Resources folder
#### Which should allow the program to build them, so if they are
#### missing, they can be used as default

import os, sys, time
from os import listdir
from os.path import isfile, join
from shutil import copy

args = sys.argv
print(args)
if not args[1]:
    print("Error: first argument must be path of main saves (eg 'allowedVersions.txt')")
    exit(1)
if not args[2]:
    print("Error: second argument must be location of packages folders")
    exit(1)
main_path = args[1]

lib = []
try:
    with open(main_path + "allowedVersions.txt", "r") as file:
        for line in file.readlines():
            print("Accepting:", line)
            lib.append(line)
except:
    print("Warning: allowedVersions.txt missing, creating with default net45")
    lib = ["net45"]
    with open(main_path + "allowedVersions.txt", "w") as file:
        file.write("net45")

def validEnding(folder):
    for ending in lib:
        if folder.endswith(ending):
            return True
    return False

packagesfolder = args[2] # "D:\Bitbucket\DogsTT_Networked\DogsTTClient\packages"

packages = [x[0] for x in os.walk(packagesfolder)]
for folder in packages:
    print(folder)
    if validEnding(folder):
        files = [join(folder, f) for f in listdir(folder) if isfile(join(folder, f))]
        for file in files:
            print(file)
            if file.endswith(".dll"):
                print("COPY:", file)
                copy(file, main_path + "\\..\\..\\Resources")

#t = input("")