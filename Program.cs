﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ultimate_puzzle
{
    public enum Style
    {
        InternalArrow_In = -1,
        InternalArrow_Out = -4,
        Octagon_In = -2,
        Octagon_Out = 2,
        Plus_In = -3,
        Plus_Out = 3,
        ExternalArrow_In = 4,
        ExternalArrow_Out = 1
    }

    public class Piece
    {
        public Style[] Sides { get; private set; }

        public Piece(Style side1, Style side2, Style side3, Style side4)
        {
            Sides = new[] {side1, side2, side3, side4};
        }

        public Style Top(int orientation)
        {
            switch (orientation)
            {
                case 0: return Sides[0];
                case 1: return Sides[1];
                case 2: return Sides[2];
                case 3: return Sides[3];
                case 4: return Sides[0];
                case 5: return Sides[3];
                case 6: return Sides[2];
                case 7: return Sides[1];
                default:
                    throw new Exception("oops");
            }
        }

        public Style Bottom(int orientation)
        {
            switch (orientation)
            {
                case 0: return Sides[2];
                case 1: return Sides[3];
                case 2: return Sides[0];
                case 3: return Sides[1];
                case 4: return Sides[2];
                case 5: return Sides[1];
                case 6: return Sides[0];
                case 7: return Sides[3];
                default:
                    throw new Exception("oops");
            }
        }

        public Style Left(int orientation)
        {
            switch (orientation)
            {
                case 0: return Sides[3];
                case 1: return Sides[0];
                case 2: return Sides[1];
                case 3: return Sides[2];
                case 4: return Sides[1];
                case 5: return Sides[0];
                case 6: return Sides[3];
                case 7: return Sides[2];
                default:
                    throw new Exception("oops");
            }
        }

        public Style Right(int orientation)
        {
            switch (orientation)
            {
                case 0: return Sides[1];
                case 1: return Sides[2];
                case 2: return Sides[3];
                case 3: return Sides[0];
                case 4: return Sides[3];
                case 5: return Sides[2];
                case 6: return Sides[1];
                case 7: return Sides[0];
                default:
                    throw new Exception("oops");
            }
        }

        public static bool Compatible(Style style1, Style style2)
        {
            return ((int) style1 + style2 == 0);
        }
    }

    public class Entry
    {
        public int Piece { get; set; }
        
        public int Orientation { get; set; }

        public Entry()
        {
            Piece = -1;
            Orientation = -1;
        }
    }
    
    class Game
    {
        public List<Piece> Pieces { get; } = new List<Piece>();
        
        public Entry[] Board = new Entry[16];
        
        private static Entry s_nullEntry = new Entry();

        public Game()
        {
            for (var entryIndex = 0; entryIndex < 16; entryIndex++)
            {
                Board[entryIndex] = new Entry();
            }
        }

        public Piece PieceAt(int entryIndex)
        {
            var pieceIndex = Board[entryIndex].Piece;
            return pieceIndex == -1 ? null : Pieces[pieceIndex];
        }
        
        public Entry EntryAbove(int entryIndex)
        {
            if (entryIndex < 0 || entryIndex > 15)
                throw new IndexOutOfRangeException();

            var location = entryIndex >= 0 && entryIndex <= 3 ? -1 : entryIndex - 4;
            return location == -1 ? s_nullEntry : Board[location];
        }
        
        public Entry EntryBelow(int entryIndex)
        {
            if (entryIndex < 0 || entryIndex > 15)
                throw new IndexOutOfRangeException();

            var location = entryIndex >= 12 && entryIndex <= 15 ? -1 : entryIndex + 4;
            return location == -1 ? s_nullEntry : Board[location];
        }
        
        public Entry EntryOnLeft(int entryIndex)
        {
            if (entryIndex < 0 || entryIndex > 15)
                throw new IndexOutOfRangeException();

            var location = entryIndex == 0 || entryIndex == 4 || entryIndex == 8 || entryIndex == 12 ? -1 : entryIndex - 1;
            return location == -1 ? s_nullEntry : Board[location];
        }
        
        public Entry EntryOnRight(int entryIndex)
        {
            if (entryIndex < 0 || entryIndex > 15)
                throw new IndexOutOfRangeException();

            var location = entryIndex == 3 || entryIndex == 7 || entryIndex == 11 || entryIndex == 15 ? -1 : entryIndex + 1;
            return location == -1 ? s_nullEntry : Board[location];
        }

        public string Dump()
        {
            return String.Join(", ", Board.Select(e => $"{e.Piece}:{e.Orientation}"));   
        }

        public override string ToString()
        {
            return Dump();
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            
            game.Pieces.AddRange(new []
            {
                new Piece(Style.Octagon_Out, Style.Plus_Out, Style.InternalArrow_Out, Style.Octagon_In),
                new Piece(Style.ExternalArrow_Out, Style.Octagon_In, Style.InternalArrow_Out, Style.ExternalArrow_Out),
                new Piece(Style.Octagon_Out, Style.ExternalArrow_In, Style.InternalArrow_Out, Style.InternalArrow_In),
                new Piece(Style.InternalArrow_Out, Style.Octagon_Out, Style.ExternalArrow_In, Style.Plus_In),
                new Piece(Style.ExternalArrow_In, Style.Octagon_In, Style.Plus_In, Style.Octagon_Out),
                new Piece(Style.InternalArrow_In, Style.Plus_In, Style.ExternalArrow_In, Style.ExternalArrow_In),
                new Piece(Style.InternalArrow_In, Style.Octagon_In, Style.ExternalArrow_In, Style.ExternalArrow_Out),
                new Piece(Style.InternalArrow_In, Style.Octagon_Out, Style.Octagon_Out, Style.Octagon_In),
                new Piece(Style.Octagon_Out, Style.Octagon_In, Style.InternalArrow_In, Style.ExternalArrow_Out),
                new Piece(Style.Plus_Out, Style.ExternalArrow_Out, Style.Plus_In, Style.InternalArrow_Out),
                new Piece(Style.ExternalArrow_Out, Style.InternalArrow_In, Style.Octagon_In, Style.Plus_Out),
                new Piece(Style.ExternalArrow_In, Style.ExternalArrow_In, Style.Octagon_In, Style.Plus_In),
                new Piece(Style.ExternalArrow_In, Style.Plus_Out, Style.InternalArrow_In, Style.InternalArrow_Out),
                new Piece(Style.Octagon_Out, Style.Plus_Out, Style.InternalArrow_In, Style.InternalArrow_In),
                new Piece(Style.Plus_In, Style.Octagon_In, Style.Octagon_Out, Style.Plus_Out),
                new Piece(Style.Octagon_In, Style.Plus_In, Style.ExternalArrow_Out, Style.Octagon_Out),
            });

            var solutions = new List<string>();
            
            Play(game, 0, g =>
            {
                solutions.Add(g.Dump());
                Console.WriteLine($"{solutions.Count}: {game.Dump()}");
            });
        }

        private static void Play(Game game, int entryIndex, Action<Game> onSolution)
        {
            if (entryIndex == 16)
            {
                onSolution(game);
                return;
            }

            for (var pieceIndex = 0; pieceIndex < 16; pieceIndex++)
            {
                // check if piece is already on the board
                if (game.Board.FirstOrDefault(e => e.Piece == pieceIndex) != null)
                    continue;
                
                // try all orientations
                for (var orientationIndex = 0; orientationIndex < 4; orientationIndex++)
                {
                    Entry other = null;
                    
                    // check entry above
                    other = game.EntryAbove(entryIndex);
                    if (other.Piece != -1)
                    {
                        if (!Piece.Compatible(
                            game.Pieces[other.Piece].Bottom(other.Orientation),
                            game.Pieces[pieceIndex].Top(orientationIndex)))
                            continue;
                    }
                    
                    // check entry below
                    other = game.EntryBelow(entryIndex);
                    if (other.Piece != -1)
                    {
                        if (!Piece.Compatible(
                            game.Pieces[other.Piece].Top(other.Orientation),
                            game.Pieces[pieceIndex].Bottom(orientationIndex)))
                            continue;
                    }
                    
                    // check entry on left
                    other = game.EntryOnLeft(entryIndex);
                    if (other.Piece != -1)
                    {
                        if (!Piece.Compatible(
                            game.Pieces[other.Piece].Right(other.Orientation),
                            game.Pieces[pieceIndex].Left(orientationIndex)))
                            continue;
                    }
                    
                    // check entry on right
                    other = game.EntryOnRight(entryIndex);
                    if (other.Piece != -1)
                    {
                        if (!Piece.Compatible(
                            game.Pieces[other.Piece].Left(other.Orientation),
                            game.Pieces[pieceIndex].Right(orientationIndex)))
                            continue;
                    }

                    // play piece
                    game.Board[entryIndex].Piece = pieceIndex;
                    game.Board[entryIndex].Orientation = orientationIndex;
                    
                    // recurse
                    Play(game, entryIndex + 1, onSolution);
                    
                    // remove piece
                    game.Board[entryIndex].Piece = -1;
                    game.Board[entryIndex].Orientation = -1;
                }
            }
        }
    }
}