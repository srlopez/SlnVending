namespace Vending
{
    public static class Configuracion
    {
        // Control de Pagos
        // cantidad máxima de cada moneda en caja
        public static int MAX_MONEDAS { get; } = 5;//10;
        // cantidad máxima de monedas admitas en el pago
        public static int MAX_PAGO_MONEDAS { get; } = 5;
        // Visualización
        public static int REFRESCO_PEQUEÑO { get; } = 60;
        // Algoritmo de Seguridad
        public static int SEGURIDAD_VALOR { get; } = 14;

    }
}