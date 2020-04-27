# 優先順位

上から順番に優先度が高い

- `x++` `x--` `x.y` `a[i]` `f(x)` 
- `++x` `--x` `+x` `-x` `!x`
- `f -> t` `f -> t @ s`
- `x * y` `x / y` `x % y`
- `x + y` `x - y`
- `x << y` `x >> y`
- `x == y` `x != y` `x <= y` `x >= y` `x < y` `x > y`
- `x & y`
- `x | y` 
- `x ^ y`
- `x && y`
- `x || y`
- `c ? t : f`
- `x = y` `x += y` `x -= y` `x *= y` `x /= y` `x %= y` `x &= y` `x |= y` `x ^= y` `x <<= y` `x >>= y` `(args) => (expr | block)`