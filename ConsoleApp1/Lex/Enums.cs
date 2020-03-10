

namespace Shpoon.Lex
{
    public enum TokenType
    {
        end = -2,
        begin = -1,

        keyword = 0, // Not used (7.2.2020)
        typeSpecifier = 1,
        identifier = 3,
        constant = 4,
        @string = 5,
        binaryOperator = 6,
        accessOperator = 7,
        preOperator = 8,
        postOperator = 9,
        prepostOperator = 10,
        comma = 11,
        @new = 12,

        lineEnd = 100,

        rBraceOpen = 101,
        rBraceClose = 102,
        cBraceOpen = 103,
        cBraceClose = 104,

        @namespace = 201,
        @class = 202,
        accessor = 203,

        ifStatement = 301,
        elseStatement = 302,
        doStatement = 303,
        whileStatement = 304,
        forStatement = 305,
        returnStatement = 306
    }
}
