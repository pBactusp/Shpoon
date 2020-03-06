

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

        lineEnd = 12,

        rBraceOpen = 13,
        rBraceClose = 14,
        cBraceOpen = 15,
        cBraceClose = 16,

        @namespace = 101,
        @class = 102,

        ifStatement = 201,
        elseStatement = 202,
        doStatement = 203,
        whileStatement = 204,
        forStatement = 205,
        returnStatement = 206
    }
}
