using UnityEngine;

namespace myProject{
    public class TileVisual : MonoBehaviour{
        private SpriteRenderer _spriteRenderer;
        private TileSpritesSo _tileSpritesSo;

        public void Initialize(TileSpritesSo tileSpritesSo){
            _tileSpritesSo = tileSpritesSo;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSprite(int index){
            _spriteRenderer.sprite = _tileSpritesSo.sprites[index];
        }
    }
}