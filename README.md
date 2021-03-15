# Loopia Dynamic DNS Client
Dynamic DNS Client for Loopia.
Based on Quartz. Using .NET Core 5.0.

Use sample.config.json in the project folder to create yourself a config.json file
Update your api username, password and domain in config.json.

Selects your first domain and updates its @ A record according to your current external IP.
This should be correct, arccording to loopias "Prepare your domain for dyndns"

https://support.loopia.se/wiki/forbereda-doman-for-vart-dyndns-stod/

Set Quartz Crontab schedule according to following examples:

https://www.freeformatter.com/cron-expression-generator-quartz.html

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

https://www.apache.org/licenses/LICENSE-2.0
