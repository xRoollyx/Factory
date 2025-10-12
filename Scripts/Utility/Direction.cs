namespace myProject{
    public enum Direction{
        N,
        Ne,
        E,
        Se,
        S,
        Sw,
        W,
        Nw
    }

    public static class DirectionExtensions{
        public static Direction Opposite(this Direction direction){
            return (int)direction < 4 ? (direction + 4) : (direction - 4);
        }

        public static Direction Previous(this Direction direction){
            return direction == Direction.N ? Direction.Nw : (direction - 1);
        }

        public static Direction Next(this Direction direction){
            return direction == Direction.Nw ? Direction.N : (direction + 1);
        }

        public static Direction Previous2(this Direction direction){
            direction -= 2;
            return direction >= Direction.N ? direction : (direction + 8);
        }

        public static Direction Next2(this Direction direction){
            direction += 2;
            return direction <= Direction.Nw ? direction : (direction - 8);
        }

        public static Direction[] Size3X3(this Direction direction){
            Direction[] size3X3 = new Direction[8];
            for (int i = 0; i < 8; i++){
                size3X3[i] = (Direction)i;
            }

            return size3X3;
        }
    }
}