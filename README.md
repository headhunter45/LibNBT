# Project Description
A C# library to read and write files using the NBT format described on http://www.minecraft.net/docs/NBT.txt

This is pretty much only useful to people making support utilities for Minecraft.

If you need a different license put create a feature request on the issue tracker and I'll probably be willing to.

I didn't see a way to add more licenses but you can also use the source under cc-by-sa.

I'll update if there are bugs, but this is pretty much done.

# Usage
The api is pretty obvious, but here's a head start.

If you want to load from a file use the static method TagCompound.ReadFromFile(filename).
If you want to save to a file use the instance method TagCompound.WriteToFile(filename).

Also for streams use TagCompound.Write and AbstractTag.Read.
