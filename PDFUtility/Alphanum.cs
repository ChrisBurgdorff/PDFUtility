using System;
using System.Collections;
using System.Text;

/*
 * Please compare against the latest Java version at http://www.DaveKoelle.com
 * to see the most recent modifications
 */
namespace AlphanumComparator
{
    public class AlphanumComparator : IComparer
    {
        private enum ChunkType { Alphanumeric, Numeric };
        private bool InChunk(char ch, char otherCh)
        {
            ChunkType type = ChunkType.Alphanumeric;

            if (char.IsDigit(otherCh))
            {
                type = ChunkType.Numeric;
            }

            if ((type == ChunkType.Alphanumeric && char.IsDigit(ch))
                || (type == ChunkType.Numeric && !char.IsDigit(ch)))
            {
                return false;
            }

            return true;
        }

        public int Compare(object x, object y)
        {
            String s1 = x as string;
            String s2 = y as string;
            if (s1 == null || s2 == null)
            {
                return 0;
            }

            int thisMarker = 0;
            int thatMarker = 0;

            long thisNumericChunk = 0, thatNumericChunk = 0;

            while ((thisMarker < s1.Length) || (thatMarker < s2.Length))
            {
                if (thisMarker >= s1.Length)
                {
                    return -1;
                }
                else if (thatMarker >= s2.Length)
                {
                    return 1;
                }
                char thisCh = s1[thisMarker];
                char thatCh = s2[thatMarker];

                StringBuilder thisChunk = new StringBuilder();
                StringBuilder thatChunk = new StringBuilder();

                while ((thisMarker < s1.Length) && (thisChunk.Length == 0 || InChunk(thisCh, thisChunk[0])))
                {
                    thisChunk.Append(thisCh);
                    thisMarker++;

                    if (thisMarker < s1.Length)
                    {
                        thisCh = s1[thisMarker];
                    }
                }

                while ((thatMarker < s2.Length) && (thatChunk.Length == 0 || InChunk(thatCh, thatChunk[0])))
                {
                    thatChunk.Append(thatCh);
                    thatMarker++;

                    if (thatMarker < s2.Length)
                    {
                        thatCh = s2[thatMarker];
                    }
                }

                int result = 0;
                // If both chunks contain numeric characters, sort them numerically
                if (char.IsDigit(thisChunk[0]) && char.IsDigit(thatChunk[0]))
                {
                    thisNumericChunk = Convert.ToInt64(thisChunk.ToString());
                    thatNumericChunk = Convert.ToInt64(thatChunk.ToString());

                    if (thisNumericChunk < thatNumericChunk)
                    {
                        result = -1;
                    }

                    if (thisNumericChunk > thatNumericChunk)
                    {
                        result = 1;
                    }
                }
                else
                {
                    result = thisChunk.ToString().CompareTo(thatChunk.ToString());
                }

                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }
    }

    public class NumericComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            string s1 = x as string;
            string s2 = y as string;

            string numS1 = "", numS2 = "";

            if (s1 == null || s2 == null)
            {
                return 0;
            }

            string lowS1 = s1.ToLower();
            string lowS2 = s2.ToLower();

            int minLength = Math.Min(lowS1.Length, lowS2.Length);
            int i = 0, j = 0, iNum = 0, jNum = 0;
            //int numDigitsS1 = 0, numDigitsS2 = 0;

            while (i < minLength && j < minLength)
            {
                if (!char.IsDigit(lowS1[i]) && !char.IsDigit(lowS2[j]))
                {
                    if (lowS1[i] == '\\' && lowS2[j] != '\\')
                    {
                        //s1 comes first
                        return -1;
                    }
                    else if (lowS2[i] == '\\' && lowS1[j] != '\\')
                    {
                        //s2 comes first
                        return 1;
                    }
                    else if (lowS1[i] < lowS2[j])
                    {
                        //s1 comes first
                        return -1;
                    }
                    else if (lowS1[i] > lowS2[j])
                    {
                        //s2 comes first
                        return 1;
                    }
                    i++; j++;
                }
                else
                {
                    if (lowS1[i] == '\\' && lowS2[j] != '\\')
                    {
                        //s1 comes first
                        return -1;
                    }
                    else if (lowS2[i] == '\\' && lowS1[j] != '\\')
                    {
                        //s2 comes first
                        return 1;
                    }
                    else if ((char.IsDigit(lowS1[i]) && !char.IsDigit(lowS2[j])) || (!char.IsDigit(lowS1[i]) && char.IsDigit(lowS2[j])))
                    {
                        if (lowS1[i] < lowS2[j])
                        {
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                    { //FILE55NEW   012345678
                        iNum = 0;
                        jNum = 0;
                        numS1 = "";
                        numS2 = "";
                        while (char.IsDigit(lowS1[i + iNum]))
                        {
                            numS1 = numS1 + lowS1[i+iNum];
                            iNum++;
                        }
                        i = i + iNum;
                        while (char.IsDigit(lowS2[j + jNum]))
                        {
                            numS2 = numS2 + lowS2[j + jNum];
                            jNum++;
                        }
                        j = j + jNum;
                        if (Convert.ToInt64(numS1) < Convert.ToInt64(numS2))
                        {
                            return -1;
                        }
                        else if (Convert.ToInt64(numS1) > Convert.ToInt64(numS2))
                        {
                            return 1;
                        }
                    }
                }
            }

            return 0;
        }
    }
}