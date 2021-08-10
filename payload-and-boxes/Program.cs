using System;

namespace payload_and_boxes {
    /*
    ****************************************************************************\
    The problem:

    Calculate the minimum number of boxes to contain a particular payload.
        - A large box can contain 4 to 6 items
        - A small box can contain only 1 item.

    Example: for a payload of 39 items, it takes 5 boxes of 6 (30), 1 box of
                five (35), and 1 box of 4 (39) for a total of 3 large boxes.

                An answer of 6 large boxes (36) and 3 single boxes (39) would
                be incorrect.

    This solution:

    Start with variables to tally the number of boxes containing 6, 5, 4, and 1
    item.

        6   5   4   1   Boxes
        -   -   -   -   ---
        0   0   0   0   0

    Calculate the number of boxes with 6 items packed using integer division.

        39 / 6 = 6 boxes of 6 and 3 boxes of 1

        6   5   4   1   Boxes
        -   -   -   -   ---
        6   0   0   3   4

    On the remaining payload, calculate the number of boxes of 5 items packed
    using integer division. There will be either 0 or 1.

        3 / 5 = 0 boxes of 5 and still 3 boxes of 1

    On the remaining payload, calculate the number of boxes of 4 items packed
    using integer division. There will be either 0 or 1.

        3 / 4 = 0 boxes of 4 and still 3 boxes of 1

        6   5   4   1   Boxes
        -   -   -   -   ---
        6   0   0   3   4

    The key to the solution is adjusting when there are 2 or 3 single boxes.
    As in the example above, if the payload is 39 calculating the number of
    boxes by seeing how many boxes of 6 you can fill will require three single
    boxes. By reducing the number of boxes with 6 or 5 items, the single boxes
    will have enough to pack in a 4 or 5 large box.

    Reduce the number of 6 pack boxes by 1

        6   5   4   1   Boxes
        -   -   -   -   ---
        5   0   0   9   14

    Now the 9 single boxes can be packed into a box of 5 and a box of 4 giving
    a total number of 7 large boxes 

        6   5   4   1   Boxes
        -   -   -   -   ---
        5   1   1   0   7

    If the payload had been 38 rather than 39, there would be 6 large boxes of 6
    and 2 single boxes for a total of 8. By reducing the number of large boxes to
    5 and using two large boxes of 4, only 7 boxes are required.

    No adjustments are needed if the calculated answer has only 1 small box as
    converting the 1 small box to 1 large box doesn't reduce the number of boxes.

    ****************************************************************************
    */
    class Program {
        void CalcBoxes(int Payload) {
            var SixPackBox = Payload / 6;
            var RemainingPayload = Payload - (SixPackBox * 6);
            var FivePackBox = RemainingPayload / 5;
            RemainingPayload -= FivePackBox * 5;
            var FourPackBox = RemainingPayload / 4;
            RemainingPayload -= FourPackBox * 4;
            var SinglePackBox = RemainingPayload;
            if (SixPackBox > 0 && SinglePackBox == 2) {
                SixPackBox--;
                FourPackBox += 2;
                SinglePackBox = 0;
            }
            if (SixPackBox > 0 && SinglePackBox == 3) {
                SixPackBox--;
                FivePackBox++;
                FourPackBox++;
                SinglePackBox = 0;
            }
            var total = SixPackBox + FivePackBox + FourPackBox + SinglePackBox;
            var large = SixPackBox + FivePackBox + FourPackBox;
            var small = SinglePackBox;
            Console.WriteLine($"p: {Payload}; {large} Large (L6:{SixPackBox}; L5:{FivePackBox}; L4:{FourPackBox}) and {small} Small (S1:{SinglePackBox}); tot:{total}");
        }
        void Run() {
            foreach(var idx in new int[] { 2, 5, 9, 17, 33, 52}) {
                CalcBoxes(idx);
            }
        }
        static void Main(string[] args) {
            new Program().Run();
        }
    }
}
