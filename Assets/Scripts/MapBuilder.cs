using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    private Tile _gameObject;
    private Map _map;
    private Camera _camera;
    private bool _tileIsActive;

	private void Awake()
	{
        _map = new Map();
	}
	/// <summary>
	/// Данный метод вызывается автоматически при клике на кнопки с изображениями тайлов.
	/// В качестве параметра передается префаб тайла, изображенный на кнопке.
	/// Вы можете использовать префаб tilePrefab внутри данного метода.
	/// </summary>
	public void StartPlacingTile(GameObject tilePrefab)
    {
        if (_tileIsActive)
        {
            Destroy(_gameObject.gameObject);
        }
        _gameObject = Instantiate(tilePrefab).GetComponent<Tile>();
        _tileIsActive = true;
    }

	private void Update()
	{
        if (!_tileIsActive)
        {
            return;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var hitInfo);

        PlacingTile(hitInfo.point, out var cellPos);

        if (Input.GetMouseButtonDown(0))
        {
            EndPlacingTile(cellPos);
        }    
	}

    private void PlacingTile(Vector3 worldPos, out Vector3Int cellPos)
    {
		cellPos = _grid.WorldToCell(worldPos);
		var cellWorldPos = _grid.GetCellCenterWorld(cellPos);
		_gameObject.transform.position = new Vector3(cellWorldPos.x, 0, cellWorldPos.z);
        var isBusy = _map.IsBusyCell(cellPos);
        _gameObject.SetBusyOrFreeColor(isBusy);
	}

    private void EndPlacingTile(Vector3Int cellPos)
    {
		if (!_map.IsBusyCell(cellPos))
		{
            _map.SetCellAsBusy(cellPos);
            _gameObject.SetDefaultColor();
            _tileIsActive = false;
            _gameObject = null;
		}
	}

	private void OnDrawGizmos()
	{
		var cellSize = _grid.cellSize;
		var cellGap = _grid.cellGap;
		var origin = _grid.transform.position;

		Gizmos.color = Color.red;

		for (int x = 0; x < 10; x++)
		{
			for (int z = 0; z < 10; z++)
			{
				var pos = origin + new Vector3(x * (cellSize.x + cellGap.x), 0, z * (cellSize.z + cellGap.z));

				Gizmos.DrawLine(pos, pos + new Vector3(cellSize.x, 0, 0));
				Gizmos.DrawLine(pos, pos + new Vector3(0, 0, cellSize.z));
				Gizmos.DrawLine(pos + new Vector3(cellSize.x, 0, 0), pos + new Vector3(cellSize.x, 0, cellSize.z));
				Gizmos.DrawLine(pos + new Vector3(0, 0, cellSize.z), pos + new Vector3(cellSize.x, 0, cellSize.z));

			}
		}
	}

}