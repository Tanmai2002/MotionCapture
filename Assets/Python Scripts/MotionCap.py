import cv2
from cvzone.PoseModule import PoseDetector

import UdpComms as U

# Create UDP socket to use for sending (and receiving)
sock = U.UdpComms(udpIP="127.0.0.1", portTX=8000, portRX=8001, enableRX=True, suppressWarnings=True)

cap = cv2.VideoCapture(0)

detector = PoseDetector()
posList = []

while True:
    success,img = cap.read()
    detector.findPose(img)
    lmList,bBoxInfo = detector.findPosition(img)

    if bBoxInfo:
        lmString = ""
        for lm in lmList:
            lmString += f'{lm[1]},{img.shape[0]-lm[2]},{lm[3]},'

        posList.append(lmString)  
        sock.SendData(lmString)  

    print(len(posList))

    cv2.imshow("Image",img)
    key = cv2.waitKey(1)

    if key == ord("s"):
        with open("AnimationFile.txt",'w') as f:
            f.writelines(["%s\n" % item for item in posList])