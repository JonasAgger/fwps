import http.client

conn = http.client.HTTPConnection("time.is", 80)
conn.request("GET", 'just')
response = conn.getresponse()

print(response.code, response.reason)