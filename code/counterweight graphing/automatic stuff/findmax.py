import glob
import csv

files = glob.glob("coords/*.csv")

print(files)

lines = []


for filename in files:
    with open(filename, "r", newline="") as f:
        glasses = csv.reader(f)
        curr = 0
        listy = []
        for row in glasses:
            if float(row[0]) > curr:
                curr = float(row[0])
    filename = filename[13:-4]
    listy = filename.split("-")
    listy.append(str(curr))
    lines.append(listy)

with open("output.csv", "w", newline="") as f:
    writer = csv.writer(f)
    writer.writerows(lines)
