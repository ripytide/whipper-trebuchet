import matplotlib.pyplot as plt
import math
import csv
import random

#constants
step = 0.001



def scrapeData(filename):
    f = open(filename, "r")
    data = f.readlines()

    #remove the top lines from the file as they are not data
    data.pop(0)
    data.pop(0)

    bigdata = []

    def correct(number):
        index = number.index("E")
        exp = number[index + 1:]
        correct_number = float(number[:index]) * 10 ** int(exp)
        
        return(correct_number)

    #removes \n and makes each line a sub array for the three quantities
    for idx in range(len(data)):
        data[idx] = data[idx].replace('\n', '')

        mini_array = data[idx].split(",")
        bigdata.append(mini_array)

        for num in range(len(bigdata[idx])):
            bigdata[idx][num] = correct(bigdata[idx][num])

    offset = bigdata[0][0]

    for i in bigdata:
        i[0] -= offset

    return (bigdata)

def getCoords(bigArray, name, xadjust=0, yadjust=0, scale=1):

    x = []
    y = []

    for i in bigArray:
        x.append(i[1] * scale - xadjust)
        y.append(i[2] * scale - yadjust)
    plt.plot(x, y, label=name)
    

def corule(b, a, th):
    return math.sqrt((b)** 2 + (a)** 2 - 2 * a * b * math.cos(th))
    
def draw(name, a, b):
    global step
    theta = []
    x = []
    y = []

    i = 0
    while i < 1:
        theta.append(i * 1 * math.pi)
        i += step


    for i in theta:
            
        if i < math.pi/4:
            c = corule(a,b,math.pi/4)
        else:
            c = corule(a,b,i)

        angle = math.acos((c**2 + b**2 - a**2) / (2 * b * c))

        x.append(c * math.cos(math.pi/2 - i - angle))
        y.append(c * math.sin(math.pi/2 - i - angle))

        
    #fix the start of the circle
    newx=[]
    newy=[]
    endangle = math.atan(y[0]/x[0])
    c = corule(a,b,math.pi/4)
    i=math.pi/2
    while i > endangle:

        newx.append(c*math.cos(i))
        newy.append(c*math.sin(i))

        i -= step


    plt.plot(x, y, label = name)
    plt.plot(newx,newy)




def getValues(fn,name):
    xvalues = []
    yvalues = []

    with open(fn,"r",newline="") as file:
        reader = csv.reader(file)
        curr = 0
        select = 1
        for row in reader:
            if curr % select == 0:
                xvalues.append(float(row[0]))
                yvalues.append(float(row[1]))
            curr += 1

    plt.plot(xvalues,yvalues, label=name)





def drawCircle(radius):
    x = []
    y=[]
    i = 0
    while i < 2 * math.pi:

        x.append(radius*math.cos(i))
        y.append(radius*math.sin(i))

        i += step

    plt.plot(x, y, label="limit: a + b")
    









attempt1 = scrapeData("attempt1 a=10 b=12.txt")
attempt2 = scrapeData("attempt2 a=10 b=12.txt")
attempt3 = scrapeData("attempt3 a=10 b=12.txt")

attempt4 = scrapeData("attempt1 a=7 b=17.txt")
attempt5 = scrapeData("attempt2 a=7 b=17.txt")
attempt6 = scrapeData("attempt3 a=7 b=17.txt")

attempt7 = scrapeData("attempt1 a=19 b=4.txt")
attempt8 = scrapeData("attempt2 a=19 b=4.txt")
attempt9 = scrapeData("attempt3 a=19 b=4.txt")

specialAttempt = scrapeData("special attempt longer path a=19 b=4.txt")

#getCoords(attempt1, "Attempt 1", 23.35, 29.8, 110)
#getCoords(attempt2, "Attempt 2", 23.35, 29.8, 110)
#getCoords(attempt3, "Attempt3 a=10 b=12", 23.35, 29.8, 110)

#getCoords(attempt4, "Attempt1 a=7 b=17", 24.1, 30.4, 110)
#getCoords(attempt5, "Attempt2 a=7 b=17", 24.1, 30.4, 110)
#getCoords(attempt6, "Attempt3 a=7 b=17", 24.1, 30.4, 110)

#getCoords(attempt7, "Attempt1 a=19 b=4", 23.58, 30.11, 110)
#getCoords(attempt8, "Attempt2 a=19 b=4", 23.58, 30.11, 110)
#getCoords(attempt9, "Attempt3 a=19 b=4", 23.58, 30.11, 110)

getCoords(specialAttempt, "a=19 b=4 longer path", 8.85, -1.95, 90)

#draw('My Model', 10, 12)

#getValues("mass=1.csv", "Simulation 1")
#getValues("mass=5 .csv", "Simulation Mass=5")
#getValues("mass=5 velocity iter = 40.csv", "Simulation Counterweight Mass x5 higher velocity iterations=40")
#getValues("mass=8 velocity iter = 40.csv", "Simulation Counterweight Mass x8 higher velocity iterations=40")
#getValues("mass=9.3 a=10 b=12 velocity iter = 40.csv", "Simulation Mass=9.3 velocity iterations=40")
#getValues("mass=9.3 velocity iter = 40 position iter = 40.csv", "Simulation Counterweight Mass x9.3 higher velocity iterations=40 position iterations=40")

#getValues("mass=9.3 velocity iter = 40 a=7 b=17.csv","Simulation Mass=9.3 velocity iterations=40 a=7 b=17")
#getValues("mass=9.3 velocity iter = 40 a=19 b=4.csv","Simulation Mass=9.3 velocity iterations=40 a=19 b=4")

getValues("mass=9.3 velocity iter = 40 a=19 b=4 special.csv", "Simulation Mass=9.3 velocity iterations=40 a=19 b=4 Longer path")

#drawCircle(16.417)
#drawCircle(23)



ox = [0]
oy = [0]
plt.grid(True)
plt.plot(ox, oy, label = 'Origin', marker = 'o', markerfacecolor = 'blue', markersize = 12)
plt.axis('equal')

plt.legend(loc='best')

plt.show()