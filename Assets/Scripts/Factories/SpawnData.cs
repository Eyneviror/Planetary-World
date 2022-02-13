
    public struct SpawnData
    {
        public Enemy EnemyObject;
        public int count;

        public static SpawnData Empty = new SpawnData() {EnemyObject = null, count = 0};
    }
