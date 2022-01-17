using DAE.BoardSystem;
using DAE.HexSystem;
using DAE.StateSystem;
using DAE.GameSystem.GameStates;
using UnityEngine;

namespace DAE.GameSystem
{
    class GameLoop : MonoBehaviour
    {

        [SerializeField]
        private PositionHelper _positionHelper;

        [SerializeField]
        private Transform _boardParent;

        private ActionManager<Card, Piece> _moveManager;
        private Piece _piece;

        public GameObject Hand;

        public Card CurrentCard;

        public GameObject StartButton;

        private StateMachine<GameStateBase> _gameStateMachine;

        public void Start()
        {

            var grid = new Grid<Position>(30);
            ConnectGrid(grid);

            var board = new Board<Position, Piece>();
            ConnectPiece(grid, board);

            _moveManager = new ActionManager<Card, Piece>(board, grid);

            _gameStateMachine = new StateMachine<GameStateBase>();
            _gameStateMachine = new StateMachine<GameStateBase>();
            _gameStateMachine = new StateMachine<GameStateBase>();

            _gameStateMachine.Register(GameState.StartScreenState,
                   new StartScreenState (_gameStateMachine, _moveManager));

            _gameStateMachine.Register(GameState.EndScreenState,
                new StartScreenState(_gameStateMachine, _moveManager));

            _gameStateMachine.Register(GameState.GamePlayState,
                   new GamePlayState(_gameStateMachine, _moveManager, Hand));

            _gameStateMachine.InitialState = GameState.StartScreenState;




            board.Moved += (s, e) =>
            {
                if (grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition =
                        _positionHelper.ToWorldPosition(
                            grid, _boardParent, toCoordinate.x, toCoordinate.y);

                    e.Piece.MoveTo(worldPosition);
                    RemoveCard();
                }
        };

        board.Placed += (s, e) =>
            {

                if (grid.TryGetCoordinateOf(e.ToPosition, out var toCoordinate))
                {
                    var worldPosition =
                        _positionHelper.ToWorldPosition(
                            grid, _boardParent, toCoordinate.x, toCoordinate.y);
                    RemoveCard();

                    e.Piece.Place(worldPosition);
                }
            };

            board.Taken += (s, e) =>
            {
                RemoveCard();
                e.Piece.Taken();

                if (e.Piece.PieceType == PieceType.Player)
                {
                    _gameStateMachine.MoveToState("gamePlayState");
                }
            };
        }

        private void GenerateStartHand()
        {
            for (int i = 0; i < Hand.GetComponent<HandHelper>().MaxHand; i++)
                Hand.GetComponent<HandHelper>().GenerateCard();
        }

        private void RemoveCard()
        {
            var cards = FindObjectsOfType<Card>();
            foreach (var card in cards)
            {
                card.CardDestory();
            }

            Hand.GetComponent<HandHelper>().GenerateCard();
            
        }

        private void ConnectGrid(Grid<Position> grid)
        {
            var views = FindObjectsOfType<PositionView>();
            foreach (var view in views)
            {
                var position = new Position();
                view.Model = position;

                var pieces = FindObjectsOfType<Piece>();
                var (x, y) = _positionHelper.ToGridPostion(grid, _boardParent, view.transform.position);
                grid.Register(x, y, position);

                foreach (var piece in pieces)
                {
                    DropCard(view, pieces, views, Hand);

                    HoverTiles(view, pieces, views, Hand);
                }
                view.gameObject.name = $"Tile ({x},{y})";
            }
        }

        private void DropCard(PositionView view, Piece[] pieces, PositionView[] views, GameObject hand)
        {
            view.Dropped += (s, e) =>
            {
                var cards = FindObjectsOfType<Card>();
                _gameStateMachine.CurrentState.Dropped(e.Position, _piece, pieces, views, cards, hand);
                //    var cards = FindObjectsOfType<Card>();
                //    foreach (var card in cards)
                //    {
                //        if (card.CardActive == true)
                //        {
                //            _moveManager.Action(_piece, e.Position, card);

                //            var views = FindObjectsOfType<PositionView>();
                //            foreach (var view in views)
                //            {
                //                view.Model.Deactivate();
                //            }
                //            RemoveCard();

                //        }
                //    }
            };
        }

        private void HoverTiles(PositionView view, Piece[] pieces, PositionView[] views, GameObject hand)
        {
            view.Hovered += (s, e) =>
            {
                if (StartButton.activeInHierarchy == false)
                {
                    _gameStateMachine.MoveToState("gamePlayState");
                }

                var cards = FindObjectsOfType<Card>();


                _gameStateMachine.CurrentState.Hovered(e.Position, _piece, pieces, views, cards, hand);

                //bool isolate = false;
                //var views = FindObjectsOfType<PositionView>();
                //foreach (var view in views)
                //{
                //    view.Model.Deactivate();
                //}

                //var cards = FindObjectsOfType<Card>();
                //foreach (var card in cards)
                //{
                //    if (card.CardActive == true)
                //    {
                //        var positions = _moveManager.ValidPositionFor(_piece, position, card);

                //        foreach (var pos in positions)
                //        {
                //            if (position == pos)
                //                isolate = true;
                //        }

                //        if (isolate != true)
                //            foreach (var pos in positions)
                //                if (pos != null)
                //                    pos.Activate();

                //        if (isolate == true)
                //        {
                //            var isolatedPos = _moveManager.IsolatedValidPositionFor(_piece, position, card);

                //            foreach (var iPos in isolatedPos)
                //            {
                //                if (iPos != null)
                //                    iPos.Activate();
                //            }
                //        }
                //    }
                //}
            };
        }

        private void ConnectPiece(Grid<Position> grid, Board<Position, Piece> board)
        {
            var pieces = FindObjectsOfType<Piece>();
            foreach (var piece in pieces)
            {
                var (x, y) = _positionHelper.ToGridPostion(grid, _boardParent, piece.transform.position);
                if (grid.TryGetPositionAt(x, y, out var position))
                {   
                    if (piece.PieceType == PieceType.Player)
                    {
                        _piece = piece;
                        board.Place(piece, position);
                    }
                    board.Place(piece, position);
                }
            }
        }

    }
}
