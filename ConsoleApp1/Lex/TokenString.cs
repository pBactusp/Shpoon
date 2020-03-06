using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Lex
{
    public class TokenString : List<Token>
    {
        public TokenString()
        {

        }
        public TokenString(Token token)
        {
            Add(token);
        }
        public TokenString(IEnumerable<Token> tokens)
        {
            AddRange(tokens);
        }

        public bool Contains(TokenType targetType)
        {
            return this.Any(t => t.Type == targetType);
        }
        public bool Match(int index, params TokenType[] pattern)
        {
            if (this.Count - index < pattern.Length)
                return false;

            for (int i = 0; i < pattern.Length; i++)
                if (this[i + index].Type != pattern[i])
                    return false;

            return true;
        }

        public List<TokenString> Split(bool keepSeparetor, params TokenType[] tokenType)
        {
            var lists = new List<TokenString>();
            var current = new TokenString();

            lists.Add(current);

            for (int i = 0; i < this.Count; i++)
            {
                if (tokenType.Contains(this[i].Type))
                {
                    if (keepSeparetor)
                        current.Add(this[i]);

                    current = new TokenString();
                    lists.Add(current);
                }
                else
                    current.Add(this[i]);
            }

            return lists;
        }

        public int FindNextIndex(int index, params TokenType[] targetTypes)
        {
            while (index < this.Count)
            {
                if (targetTypes.Contains(this[index].Type))
                    return index;

                index++;
            }

            return -1;
        }

        public TokenString GetRangeTStr(int index, int count)
        {
            return new TokenString(GetRange(index, count));
        }
        public TokenString GetRangeTStr(int index)
        {
            return new TokenString(GetRange(index, Count - index));
        }


        public TokenString GetRangeWhile(int index, params TokenType[] validTypes)
        {
            var range = new TokenString();

            while (validTypes.Contains(this[index].Type))
            {
                range.Add(this[index]);
                index++;
            }

            return range;
        }
        public TokenString GetRangeUntil(int index, params TokenType[] invalidTypes)
        {
            var range = new TokenString();

            while (index < Count && !invalidTypes.Contains(this[index].Type))
            {
                range.Add(this[index]);
                index++;
            }

            return range;
        }
        public TokenString GetRangeUntil(ref int index, params TokenType[] invalidTypes)
        {
            var range = new TokenString();

            while (!invalidTypes.Contains(this[index].Type))
            {
                range.Add(this[index]);
                index++;
            }

            return range;
        }


        public TokenString GetRangeInBrackets(int index)
        {
            TokenType openType = this[index].Type;
            TokenType closeType = (TokenType)((int)openType + 1);

            var range = new TokenString();
            int openedNum = 1;
            index++;

            while (index < this.Count)
            {
                if (this[index].Type == openType)
                    openedNum++;
                else if (this[index].Type == closeType)
                {
                    openedNum--;

                    if (openedNum == 0)
                        return range;
                }

                range.Add(this[index]);

                index++;
            }

            return null;
        }
        public TokenString GetRangeInBrackets(ref int index)
        {
            TokenType openType = this[index].Type;
            TokenType closeType = (TokenType)((int)openType + 1);

            var range = new TokenString();
            int openedNum = 1;
            index++;

            while (index < this.Count)
            {
                if (this[index].Type == openType)
                    openedNum++;
                else if (this[index].Type == closeType)
                {
                    openedNum--;

                    if (openedNum == 0)
                    {
                        index++;
                        return range;
                    }
                }

                range.Add(this[index]);

                index++;
            }

            return null;
        }

        public int GetClosingIndex(int index)
        {
            TokenType openType = this[index].Type;
            TokenType closeType = (TokenType)((int)openType + 1);

            int openedNum = 1;
            index++;

            while (index < this.Count)
            {
                if (this[index].Type == openType)
                    openedNum++;
                else if (this[index].Type == closeType)
                {
                    openedNum--;

                    if (openedNum == 0)
                        return index;
                }

                index++;
            }

            return -1;
        }
    }
}
