def dispMap(data, width, height):
    print("\n\n\n-----------------------------")
    dataArr = data.split(',')
    for y in range(height):
        line = ""
        for x in range(width):
            line += dataArr[y*height+x]
        print(line)
    print("\n\n\n-----------------------------")

while True:
    dispMap(input("Data: "), int(input("Width: ")), int(input("Height: ")))
