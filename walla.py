import http.client
import json


conn = http.client.HTTPConnection("fwps.azurewebsites.net", 80)
conn.request("GET", "/api/Light/next", headers={"Content-Type": "application/json"})
response = conn.getresponse()

print(response.status, response.reason)

string = response.read().decode('utf-8')
msg = json.loads(string)

print(msg["id"], msg["command"])

body = {}

body["id"] = msg["id"]
body["isRun"] = True

body = json.dumps(body)

conn.request("PUT", "/api/light/"+str(msg["id"]), body=body, headers={"Content-Type": "application/json"})
response = conn.getresponse()

print(response.status, response.reason) 
