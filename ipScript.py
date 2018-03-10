import http.client
import json
import subprocess
import string

result = subprocess.run(['hostname', '-I'], stdout=subprocess.PIPE)

body = {
    "id": 1,
    "ip": "50.50.50.50"
}


msg = ""
msg = result.stdout
msg = msg.strip().decode('ascii', 'strict')

print(msg)


body["ip"] = msg

body = json.dumps(body)

print(body)

conn = http.client.HTTPConnection("fwps.azurewebsites.net", 80)
conn.request("PUT", "/api/ip/1", body=body, headers={"Content-Type": "application/json"})
response = conn.getresponse()



print(response.status, response.reason)




