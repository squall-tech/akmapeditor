 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ProtoBuf;

namespace AKMapEditor.OtMapEditor
{

    public class ChangeType
    {
        public const uint NO_CHANGE = 0;
        public const uint CHANGE_TILE = 1;     
    };

    public enum ActionIdentifier {
	    ACTION_MOVE,
	    ACTION_SELECT,
	    ACTION_DELETE_TILES,
	    ACTION_CUT_TILES,
	    ACTION_PASTE_TILES,
	    ACTION_RANDOMIZE,
	    ACTION_BORDERIZE,
	    ACTION_DRAW,
	    ACTION_SWITCHDOOR,
	    ACTION_ROTATE_ITEM,
	    ACTION_CHANGE_PROPERTIES,
    };

    [ProtoContract]
    public class Change
    {
        [ProtoMember(1)]
        public uint Type { get; set; }
      //  [ProtoMember(2)]
    //    public Tile Data { get; set; }

        [ProtoMember(2)]
        public Tile CommitData { get; set; }
        [ProtoMember(3)]
        public Tile UndoData { get; set; }
        
        public Change(Tile tile)
        {
            Type = ChangeType.CHANGE_TILE;
          //  Data = tile;  
            CommitData = tile;
        }

        public Change deepCopy()
        {
            Change change = new Change();
            change.Type = this.Type;
            bool isEmpty = true;
            if (this.CommitData != null)
            {
                isEmpty = false;
                change.CommitData = this.CommitData.deepCopy();
            }
            if (this.UndoData != null)
            {
                isEmpty = false;
                change.UndoData = this.UndoData.deepCopy();
            }
            if (!isEmpty)
            {
                return change;
            }
            else
            {
                return null;                   
            }
        }

        public Change()
        {
        }
    }


    [ProtoContract]
    public class Action
    {
        private GameMap map;
        private MapEditor editor;
        private bool commited = false;

        [ProtoMember(1)]
        public List<Change> changeList;

        public Action(MapEditor editor)
        {
            changeList = new List<Change>();
            this.map = editor.gameMap;
            this.editor = editor;
        }        

        public Action()
        {
            
        }

        public Action deepCopy()
        {
            Action action = new Action();
            action.changeList = new List<Change>();
            bool isEmpty = true;
            foreach (Change change in changeList)
            {
                Change changeCopy = change.deepCopy();                
                if (changeCopy != null)
                {
                    isEmpty = false;
                    action.addChange(changeCopy);
                }
                
            }
            if (!isEmpty)
            {
                return action;
            }
            else
            {
                return null;
            }
            
        }

        public void SetMap(GameMap map)
        {
            this.map = map;
        }

        public void SetEditor(MapEditor editor)
        {
            this.editor = editor;
            if (this.editor != null)
            {
                this.map = editor.map;
            }
            
        }

        public void addChange(Change t)
        {        
            changeList.Add(t);
        }        

	    public void commit()
        {
            foreach (Change c in changeList)
            {
                switch (c.Type)
                {
                    case ChangeType.CHANGE_TILE:
                        //Tile newtile = c.Data;


                        Tile newtile = c.CommitData;
                        Position pos = newtile.Position;
                        Tile oldTile = map.swapTile(pos, newtile);
                        newtile.update();

                        if (newtile.isSelected())
                            editor.selection.addInternal(newtile);

                        if (oldTile != null)
                        {

                            if (oldTile.spawn != null) 
                            {
                                if (newtile.spawn != null)
                                {
                                    if (oldTile.spawn.getSize() != newtile.spawn.getSize())
                                    {
                                        map.RemoveSpawn(oldTile);
                                        map.AddSpawn(newtile);
                                    }
                                }
                                else
                                {
                                    // Spawn has been removed
                                    map.RemoveSpawn(oldTile);
                                }
                            }
                            else if (newtile.spawn != null)
                            {
                                map.AddSpawn(newtile);
                            }

                            if (oldTile.isSelected())
                                editor.selection.removeInternal(oldTile);                              
                            //c.Data = oldTile; 
                            c.UndoData = oldTile;
                        }
                        else
                        {                            
                            //c.Data = new Tile(pos);
                            c.UndoData = new Tile(pos);

                            if (newtile.spawn != null)
                                map.AddSpawn(newtile);

                        }
                        break;
                }
                commited = true;
            }
        }
	    public bool isCommited() 
        {
            return commited;
        }

	    public void undo()
        {
            foreach (Change it in changeList.Reverse<Change>())
            {
                switch (it.Type)
                {
                    case ChangeType.CHANGE_TILE:
                      //  Tile oldtile = (Tile)it.Data;
                        Tile oldtile = it.UndoData;
                        Tile newTile = map.swapTile(oldtile.Position, oldtile);
				        if(oldtile.isSelected())
					        editor.selection.addInternal(oldtile);
                        if (newTile.isSelected())
                            editor.selection.removeInternal(newTile);

                        if (oldtile.spawn != null)
                        {
                            if (newTile.spawn != null)
                            {
                                if (oldtile.spawn.getSize() != newTile.spawn.getSize())
                                {
                                    editor.map.RemoveSpawn(newTile);
                                    editor.map.AddSpawn(oldtile);
                                }
                            }
                            else
                            {
                                editor.map.AddSpawn(oldtile);
                            }
                        }
                        else if (newTile.spawn != null)
                        {
                            editor.map.RemoveSpawn(newTile);
                        }
                        //it.Data = newTile;                                                
                        //it.CommitData = newTile;
                        break;
                }
            }
        }   
	    public void redo() {
            commit();
        }
    }

    [ProtoContract]
    public class BatchAction
    {
        [ProtoMember(1)]
        public List<Action> actions;

        public ActionIdentifier type;
        public DateTime datetime;

        public BatchAction()
        {

        }

        public BatchAction DeepCopy()
        {
            bool isEmpty = true;
            BatchAction batchAction = new BatchAction();
            batchAction.actions = new List<Action>();
            if (this.type != ActionIdentifier.ACTION_SELECT)
            {                
                batchAction.type = this.type;
                foreach (Action action in actions)
                {
                    Action copyAction = action.deepCopy();
                    if (copyAction != null)
                    {
                        isEmpty = false;
                        batchAction.addAction(copyAction);
                    }
                    
                }
                
            }
            if (!isEmpty)
            {
                return batchAction;
            }
            else 
            {
                return null;
            }
            
        }

        public void resetTimer() 
        { 
            datetime = DateTime.Now.AddYears(-1); 
        }

        public BatchAction(ActionIdentifier type)
        {
            this.type = type;
            actions = new List<Action>();
        }

        public void addAction(Action action)
        {          
            actions.Add(action);
            datetime = DateTime.Now;
        }

        public void addAndCommitAction(Action action)
        {
            addAction(action);
            commitActions();
        }

        public void commitActions()
        {
            foreach (Action action in actions)
            {
                if (!action.isCommited())
                {
                    action.commit();
                }
            }
        }

        public void merge(BatchAction other)
        {
            foreach (Action action in other.actions)
            {
                actions.Add(action);
            }
          //  other.actions.Clear();
        }
    }


    public class ActionQueue
    {
        private MapEditor editor;
        private UInt16 current = 0;
        private List<BatchAction> actions;

        public ActionQueue(MapEditor editor)
        {
            this.editor = editor;
            actions = new List<BatchAction>();
        }

        public void resetTimer()
        {
            if (actions.Count > 0)
            {
                actions.Last<BatchAction>().resetTimer();
            }
        }

        public void addBatch(BatchAction batchAction, int stacking_delay)
        {
            if (batchAction == null) return;
            batchAction.commitActions();

            while (current != actions.Count)
            {
                BatchAction action = actions.Last();
                actions.Remove(action);
            }

            int undoSize = Settings.GetInteger(Key.UNDO_SIZE);

            if (undoSize < actions.Count)
            {
                actions.RemoveAt(0);
                current--;
            }

            //aqui tu vai adicionar numa fila de batchs pra commit, se ele atualiza o mapa aqui.
            //tem que atualizar no servidor igual
            //se ele voltar aqui agrupado, terá que voltar agrupado la também.
            //as rotinas vão ser as mesmas      

            editor.UpdateServer(batchAction, UpdateType.UPDATE_TYPE_COMMIT);

            if (actions.Count > 0)
            {                
                BatchAction lastAction = actions.Last<BatchAction>();
                if ((lastAction.type == batchAction.type) &&
                    (Settings.GetBoolean(Key.GROUP_ACTIONS)) &&
                    (DateTime.Now.AddSeconds(-stacking_delay) < lastAction.datetime))
                {
                    lastAction.merge(batchAction);
                    lastAction.datetime = DateTime.Now;
                    return;
                }
            }                       
            batchAction.datetime = DateTime.Now;
            actions.Add(batchAction);
            current++;
                
        }

        public void addAction(ActionIdentifier ident, Action action, int stacking_delay)
        {
            BatchAction a = new BatchAction(ident);
            a.addAction(action);
            addBatch(a, stacking_delay);            
        }

        public void undo()
        {
            if (current > 0)
            {               
                current--;
                BatchAction batch = actions[current];
                editor.UpdateServer(batch, UpdateType.UPDATE_TYPE_UNDO);                
                foreach (Action action in batch.actions.Reverse<Action>())
                {                    
                    action.undo();
                }
            }

        }
        public void redo()
        {            
            if (current < actions.Count)
            {
                BatchAction batch = actions[current];
                editor.UpdateServer(batch, UpdateType.UPDATE_TYPE_REDO);
                foreach (Action action in batch.actions)
                {
                    action.redo();
                }
                current++;
            }
        }

        public bool canUndo() { return current > 0; }
        public bool canRedo() { return current < actions.Count; }
    }
}
