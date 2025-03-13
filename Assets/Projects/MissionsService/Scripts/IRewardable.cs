using UnityEngine;

namespace MissionsService
{
    public interface IRewardable
    {
        // Probar si podemos saber que recompensa es, solo retornar la interfaz, lo dudo
        public int Id { get; set; }
        public string Title { get; set; }
        public Sprite Icon { get; set; }
        public float Amount { get; set; }
    }
}