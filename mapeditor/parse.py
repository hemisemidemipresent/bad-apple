# this is inefficient as fuckkkkkkk
from PIL import Image
string = ""
xCount = 44
yCount = 55

for i in range(6572):
    image = Image.open("/2hu/frames/frame"+str(i+1)+".png") # path to every frame of bad apple
    pixel_data = image.load()
    arr = []
    for i in range(xCount):
        for j in range(yCount):
            x = round(480 / xCount * i, 0);
            y = round(360 / yCount * j, 0);
            pixel = pixel_data[x, y]
            red, green, blue = pixel

            if red < 128:
                arr.append(0)
            else:
                arr.append(1)
    string += ''.join([str(x) for x in arr])

f = open("bad.txt", "a")
f.write(string)
f.close()

print(len(arr))