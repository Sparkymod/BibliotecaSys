﻿namespace BibliotecaSys.API.Extensions;

public static class NumbersExtensions
{
    public static sbyte ToSbyte(this bool input) => input ? (sbyte)1 : (sbyte)0;
    public static sbyte ToSbyte(this string input) => string.IsNullOrEmpty(input) && input is "1" or "0" ? (sbyte)1 : (sbyte)0;
    public static bool ToBool(this sbyte input) => input is 1;
    public static bool ToBool(this string input) => string.IsNullOrEmpty(input) && input is "true" or "false";
}