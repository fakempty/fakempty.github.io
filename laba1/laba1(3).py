def is_valid_pos(x,y):
    if 1<= x <=8 and 1<= y <=8:
        return True
    print("incorect pos")
    return False

def is_valid_pos2(pos1,pos2,pos3):
    if pos1 != pos2 and pos1 !=pos3 and pos2!= pos3:
        return True
    print("cant place 2 figuras in 1 place")
    return False

def q_attack(qx,qy,tx,ty):
    if qx == tx or qy == ty or abs(qx-tx) == abs(qy-ty):
        return True
    return False

def r_attack(rx,ry,tx,ty):
    if rx == tx or ry == ty:
        return True
    return False

def p_attack(px,py,tx,ty):
    if ty == py - 1 and (tx == px - 1 or tx == px + 1):
        return True
    return False

while True:
    qx , qy=map(int ,input("enter x;y of queen :").split())
    rx , ry=map(int ,input("enter x;y of rook :").split())
    px , py=map(int ,input("enter x;y of pawn :").split())

    if not (is_valid_pos(qx, qy) and is_valid_pos(rx, ry) and is_valid_pos(px, py)):
        continue

    if not (is_valid_pos2((qx, qy), (rx, ry), (px, py))):
        continue
    
    break

while True:
    while True:
        choice = int(input("chose first move: 4-exit; 1-queen; 2-rook; 3-pawn -"))
        if 1 <= choice <=4:
            break


    if choice == 1:
        if q_attack(qx,qy,px,py):
            print("queen atack pawn")
        elif p_attack(px,py,rx,ry):
            print("queen defend rook")
        else:
            print("simple move")

    elif choice == 2:
        if r_attack(rx,ry,px,py):
            print("rook attack pawn")
        elif q_attack(qx,qy,px,py):
            print("rook defend queen")
        else:
            print("simple move")

    elif choice == 3:
        if p_attack(px,py,qx,qy):
            print("pawn attack queen")
        elif p_attack(px,py,rx,ry):
            print("pawn attack rook")
        else:
            print("simple move")

    elif choice == 4:
        break



    