using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]// Estructura visible en el Inspector para configurar cada capa del fondo
    public struct ParallaxLayer
    {
        public SpriteRenderer layerRenderer; // La imagen/capa visual

        [Range(0f, 1f)]
        public float parallaxFactor; // Multiplicador de velocidad(0 = lejano/fijo, 1 = cercano/mueve con cámara)
    }

    [Header("Configuración de Capas")]
    [SerializeField] private ParallaxLayer[] parallaxLayers; // Lista/Array de todas las capas del mapa

    [Header("Referencias")]
    [SerializeField] private Transform mainCamera; // Referencia a la cámara principal

    private Vector3 lastCameraPosition; // Guarda la posición que tenía la cámara en el frame anterior
    private float[] tileWidths;
    private void Start()
    {
        // Si no asignaste la cámara manualmente en el Inspector, la busca automáticamente
       
            if (mainCamera == null)
            {
                mainCamera = Camera.main.transform;
            }
        // Guarda la posición inicial de la cámara para empezar a calcular el movimiento
        if (mainCamera != null)
        {
            lastCameraPosition = mainCamera.position;
        }

        tileWidths = new float[parallaxLayers.Length];
        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            SpriteRenderer layerSprite = parallaxLayers[i].layerRenderer;

            if (layerSprite == null)
                continue;

            Vector2 spriteSize = layerSprite.sprite.bounds.size;
            layerSprite.drawMode = SpriteDrawMode.Tiled;
            layerSprite.size = new Vector2(spriteSize.x *9f, spriteSize.y);
            tileWidths[i] = spriteSize.x * layerSprite.transform.lossyScale.x;
        }
    }
    // LateUpdate se ejecuta DESPUÉS de que el jugador y la cámara se hayan movido en Update
    private void LateUpdate()
    {
        if (mainCamera == null)
            return;

        // Calcula cuánto se movió la cámara desde el frame anterior hasta este exacto momento
        Vector3 cameraDelta =
            mainCamera.position - lastCameraPosition;

        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            if (parallaxLayers[i].layerRenderer != null)
            {
                Transform layer =
                    parallaxLayers[i].layerRenderer.transform;

                // Mueve la capa en X proporcionalmente a su parallaxFactor
                layer.position += new Vector3(
                    cameraDelta.x * (1f - parallaxLayers[i].parallaxFactor),
                    cameraDelta.y,
                    0f);

                float offset = mainCamera.position.x - layer.position.x;

                if (offset > tileWidths[i])
                    layer.position += new Vector3(tileWidths[i], 0f, 0f);
                else if (offset < -tileWidths[i])
                    layer.position -= new Vector3(tileWidths[i], 0f, 0f);
               
            }
        }
        // Actualiza la posición anterior de la cámara para el siguiente frame
        lastCameraPosition = mainCamera.position;
    }
}