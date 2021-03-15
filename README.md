# Loopia Dynamic DNS Client
Dynamic DNS Client for Loopia.
Based on Quartz.

Set your username, password and domain in config.json.

Selects your first domain and updates it's @ A record according to your current external IP.

Set Quartz Crontab schedule according to following examples:
https://www.freeformatter.com/cron-expression-generator-quartz.html

{

  "Settings": {
  
    "LoopiaAPIUri": "https://api.loopia.se/RPCSERV",
    
    "Username": "USERNAME@loopiaapi",
    
    "Password": "PASSWORD",
    
    "Domain": "DOMAIN.EXAMPLE",
    
    "Subdomain": "@",
    
    "TTL": 300,
    
    "Priority": 0,
    
    "RData": "type=A",
    
    "IPRequestUri": "https://api.ipify.org?format=json",
    
    "Schedule": "* * * ? * *",
    
    "Type": "A",
    
    "RecordId":  "" 
    
  }
  
}

# Acknowledgements
Following components are being used:

## Quartz .NET:
https://github.com/quartznet

## XML-RPC Client:
https://github.com/Horizon0156/XmlRpc

## Ipify API
https://api.ipify.org


# License

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License.
