using System;
using System.Collections.Generic;
using System.Linq;

namespace ultimate_puzzle
{
    public enum Style
    {
        InwardArrow_In = -1,
        OutwardArrow_Out = 1,
        Octagon_In = -2,
        Octagon_Out = 2,
        Plus_In = -3,
        Plus_Out = 3,
        InwardArrow_Out = 4,
        OutwardArrow_In = -4
    }

    public class Piece
    {
        private Style[] Sides { get; }

        public Piece(Style side1, Style side2, Style side3, Style side4)
        {
            Sides = new[] { side1, side2, side3, side4 };
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
            return ((int)style1 + style2 == 0);
        }
    }

    public class Entry
    {
        public Piece Piece { get; set; }

        public int Orientation { get; set; }

        public Entry()
        {
            Piece = null;
            Orientation = -1;
        }
    }

    class Game
    {
        public List<Piece> Pieces { get; } = new List<Piece>();

        public Entry[] Board { get; } = new Entry[16];

        private static readonly Entry s_nullEntry = new Entry();

        public Game()
        {
            for (var entryIndex = 0; entryIndex < 16; entryIndex++)
            {
                Board[entryIndex] = new Entry();
            }
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
            return string.Join(", ", Board.Select(e => $"{Pieces.IndexOf(e.Piece)}:{e.Orientation}"));
        }

        public override string ToString()
        {
            return Dump();
        }
    }

    static class Program
    {
        private static void Main()
        {
            var game = new Game();

            game.Pieces.AddRange([
                new Piece(Style.Octagon_Out, Style.Plus_Out, Style.OutwardArrow_In, Style.Octagon_In),
                new Piece(Style.OutwardArrow_Out, Style.Octagon_In, Style.OutwardArrow_In, Style.OutwardArrow_Out),
                new Piece(Style.Octagon_Out, Style.InwardArrow_Out, Style.OutwardArrow_In, Style.InwardArrow_In),
                new Piece(Style.InwardArrow_Out, Style.Plus_In, Style.OutwardArrow_In, Style.Octagon_Out),
                new Piece(Style.InwardArrow_Out, Style.Octagon_In, Style.Plus_In, Style.Octagon_Out),
                new Piece(Style.InwardArrow_In, Style.Plus_In, Style.InwardArrow_Out, Style.InwardArrow_Out),
                new Piece(Style.InwardArrow_In, Style.Octagon_In, Style.InwardArrow_Out, Style.OutwardArrow_Out),
                new Piece(Style.InwardArrow_In, Style.Octagon_Out, Style.Octagon_Out, Style.Octagon_In),
                new Piece(Style.Octagon_Out, Style.Octagon_In, Style.InwardArrow_In, Style.OutwardArrow_Out),
                new Piece(Style.Plus_Out, Style.OutwardArrow_Out, Style.Plus_In, Style.OutwardArrow_In),
                new Piece(Style.OutwardArrow_Out, Style.InwardArrow_In, Style.Octagon_In, Style.Plus_Out),
                new Piece(Style.InwardArrow_Out, Style.InwardArrow_Out, Style.Octagon_In, Style.Plus_In),
                new Piece(Style.InwardArrow_Out, Style.Plus_Out, Style.InwardArrow_In, Style.OutwardArrow_In),
                new Piece(Style.Octagon_Out, Style.Plus_Out, Style.InwardArrow_In, Style.InwardArrow_In),
                new Piece(Style.Plus_In, Style.Octagon_In, Style.Octagon_Out, Style.Plus_Out),
                new Piece(Style.Octagon_In, Style.Plus_In, Style.OutwardArrow_Out, Style.Octagon_Out),
            ]);

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

            foreach (var piece in game.Pieces)
            {
                // check if piece is already on the board
                if (game.Board.Any(e => e.Piece == piece))
                    continue;

                // try all orientations
                for (var orientationIndex = 0; orientationIndex < 4; orientationIndex++)
                {
                    // check entry above
                    var other = game.EntryAbove(entryIndex);
                    if (other.Piece != null)
                    {
                        if (!Piece.Compatible(
                            other.Piece.Bottom(other.Orientation),
                            piece.Top(orientationIndex)))
                            continue;
                    }

                    // check entry on left
                    other = game.EntryOnLeft(entryIndex);
                    if (other.Piece != null)
                    {
                        if (!Piece.Compatible(
                            other.Piece.Right(other.Orientation),
                            piece.Left(orientationIndex)))
                            continue;
                    }

                    // play piece
                    game.Board[entryIndex].Piece = piece;
                    game.Board[entryIndex].Orientation = orientationIndex;

                    // recurse
                    Play(game, entryIndex + 1, onSolution);

                    // cleanup
                    game.Board[entryIndex].Piece = null;
                    game.Board[entryIndex].Orientation = -1;
                }
            }
        }
    }
}
