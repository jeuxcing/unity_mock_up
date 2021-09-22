
import time
import random
import zmq

context = zmq.Context()
socket = context.socket(zmq.PUB)
socket.bind("tcp://*:12346")
time_interval = 0.001

while True:
    # send message every time_interval seconds

    # single Led Horizontal
    mes =   b'LH' + \
            random.randint(0,4).to_bytes(1, byteorder='little') + \
            random.randint(0,3).to_bytes(1, byteorder='little') + \
            random.randint(0,23).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            b'0'
    
    #print(mes)
    socket.send(mes)
    time.sleep(time_interval)

    # single Led Vertical
    mes =   b'LV' + \
            random.randint(0,3).to_bytes(1, byteorder='little') + \
            random.randint(0,4).to_bytes(1, byteorder='little') + \
            random.randint(0,23).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            b'0'
    
    #print(mes)
    socket.send(mes)
    time.sleep(time_interval)

    # single Led Ring
    mes =   b'LR' + \
            random.randint(0,4).to_bytes(1, byteorder='little') + \
            random.randint(0,4).to_bytes(1, byteorder='little') + \
            random.randint(0,11).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            b'0'
    
    #print(mes)
    socket.send(mes)
    time.sleep(time_interval)

    # whole Segment Horizontal
    mes =   b'SH' + \
            random.randint(0,4).to_bytes(1, byteorder='little') + \
            random.randint(0,3).to_bytes(1, byteorder='little') + \
            random.randint(0,10).to_bytes(1, byteorder='little') + \
            random.randint(11,23).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            b'0'
    
    #print(mes)
    socket.send(mes)
    time.sleep(time_interval)

    # whole Segment Vertical
    mes =   b'SV' + \
            random.randint(0,3).to_bytes(1, byteorder='little') + \
            random.randint(0,4).to_bytes(1, byteorder='little') + \
            random.randint(0,10).to_bytes(1, byteorder='little') + \
            random.randint(11,23).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            b'0'
    
    #print(mes)
    socket.send(mes)
    time.sleep(time_interval)

    # whole Segment Ring
    mes =   b'SR' + \
            random.randint(0,4).to_bytes(1, byteorder='little') + \
            random.randint(0,4).to_bytes(1, byteorder='little') + \
            random.randint(0,5).to_bytes(1, byteorder='little') + \
            random.randint(6,11).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            random.randint(0,255).to_bytes(1, byteorder='little') + \
            b'0'
    
    #print(mes)
    socket.send(mes)
    time.sleep(time_interval)

    