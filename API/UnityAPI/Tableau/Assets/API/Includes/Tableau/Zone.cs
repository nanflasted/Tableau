using System;
using System.Collections;
using UnityEngine;

namespace Tableau {
    public abstract class Zone : GameObject {
        public Player owner;
        public GameObject prefab;
        // TODO public List</*unityAnimationType*/> animations; copied lol
        // TODO public List</*unitySoundType*/> sounds; copied lol

        public Zone() {
           this(null, null);
        }

        public Zone(Player owner, GameObject prefab) {
            this.owner = owner;
            this.prefab = prefab;
        }

        public abstract Piece getPiece();
        public abstract void addPiece(Piece p);
        public abstract void clear();
        public abstract bool isEmpty();

        public virtual void Start() {}  // TODO idk
        public virtual void Update() {} // TODO play anims and sounds
    }

    public class PieceZone : Zone {
        private Piece occupant;
        // TODO card/piece orientations? in subclasses?

        public PieceZone() {
            this(null, null, null);
        }

        public PieceZone(Player owner, GameObject prefab, Piece occupant) {
            super(owner, prefab);
            this.occupant = occupant;
        }
        
        public virtual Piece getPiece() { return occupant; }
        
        public virtual void addPiece(Piece p) { occupant = p; }

        public virtual void clear() { occupant = null; }

        public virtual bool IsEmpty() { return occupant == null; }
    }

    // TODO custom class for zones w/ many cards? e.g. a deck zone but could be a piece
}