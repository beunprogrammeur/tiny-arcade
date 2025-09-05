# tiny-arcade
An arcade frontend / backend system prototype

Before running opening the Visual Studio project *for the first time*, in `PowerShell` run the `set-jwt.ps1` script
This script will ask you for a key/password to sign the API login tokens with
It will be active for the current user. to view the key, simply run `get-jwt.ps1` 

The API will then use that value as your token, it is recommended to make longer keys, 
but as it runs locally, I mainly did it this way so we don't have the key saved in the repository.

if you've already opened visual studio, simply close it and reopen for the key to be picked up.


# testing the API

if you want to test the API, run requests etc, I've included a bruno collection.
you can find bruno at [it's own website](https://www.usebruno.com/)
