import sys
import socket
import subprocess
import http.client
import json

PORT = 9000
BUFSIZE = 1000

deviceDict = {}

def readTextTCP(client):
    text = ""
    ch = client.recv(1)

    while True:
        if ch.decode('ascii', 'strict') == '\r':
            if (client.recv(1)).decode('ascii', 'strict') == '\n':
                return text
        text += ch.decode('ascii', 'strict')
        ch = client.recv(1)


def writeTextTCP(text,  client):
    client.send((text+"\0").encode('ascii', 'strict'))

def getCommand():
    conn = http.client.HTTPConnection("fwps.azurewebsites.net", 80)
    conn.request("GET", "/api/light/next", headers={"Content-Type": "application/json"})
    response = conn.getresponse()

    if response.status == 404:
         return (-1, "pass")

    string = response.read().decode('utf-8')
    msg = json.loads(string)

    return (msg["id"], msg["command"])

def updateCommand(id):
    if id == -1:
        return
    body = {}
    body["id"] = id
    body["isRun"] = True

    body = json.dumps(body)

    conn = http.client.HTTPConnection("fwps.azurewebsites.net", 80)
    conn.request("PUT", "/api/light/"+str(id), body=body, headers={"Content-Type": "application/json"})
    response = conn.getresponse()
    #if response.status is not 204:
    #   updateCommand(id)
    return

def main():
    result = subprocess.run(["hostname", "-I"], stdout=subprocess.PIPE)
    HOST = ""
    HOST = result.stdout
    HOST = HOST.strip().decode('ascii', 'strict')

    serversocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    serversocket.bind((HOST, PORT))

    print("Setting up server on IP: {}, on port {}".format(HOST, PORT))

    serversocket.listen(5)


    while True:
        # Accepting Connection
        clientsocket, addr = serversocket.accept()

        id, command = getCommand()

        print("got a connection from {}".format(addr))

        msg = readTextTCP(clientsocket)

        if not msg in deviceDict:
            deviceDict[msg] = addr
            print("added ", msg, " at ", addr, " to dict")

        # Getting file request


        print("message received: {}".format(msg))

        # If close, close server, used for debugging and quick resetting the server
        if msg == "close":
            serversocket.close()
            sys.exit()

        msg = "off\r\n"

        if command == "on":
            msg = "on\r\n"
        elif command == "pass":
            msg = "pass"

        print(msg)

        writeTextTCP(msg, clientsocket)

        updateCommand(id)

        clientsocket.close()

main()
