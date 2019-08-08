cd ../
dotnet build -c Release
dotnet pack -c Release -o nuget
cd nuget