using UnityEngine;

namespace MissionsService
{
    public interface IRewardable
    {
        // Probar si podemos saber que recompensa es, solo retornar la interfaz, lo dudo
        public string Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public int Amount { get; set; }
    }
}