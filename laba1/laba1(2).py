import random 

def shoot(min=-5, max=5):
    x=random.randint(min, max)
    y=random.randint(min, max)
    return x, y

def hit(x,y,r):
    if x>=0 and y>=0:
        rpow=pow(r,2)
        sum1=pow(x,2)+pow(y,2)
        if rpow >= sum1:
            print("first 1")
            return True
    elif x<=0 and y<=0:
        rpow=pow(r,2)
        sum1=2*rpow+x+y
        if rpow >= sum1:
            print("first 2")
            return True
    return False

def main():
    print("№".ljust(7)+"x y".ljust(7)+ "result")
    r=3

    for i in range(1,11):
        x,y=shoot()
        if hit(x,y,r):
            print("№"+str(i).ljust(4)+str (x).ljust(3)+ ":"+ str (y).ljust(6)+ "in".ljust(6))
        else:
            print("№"+str(i).ljust(4)+str (x).ljust(3)+ ":"+ str (y).ljust(6)+ "out".ljust(6))

if __name__ == "__main__":
    main()