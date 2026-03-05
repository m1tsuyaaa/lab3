using System;
using System.Text;

class MatrixException : Exception
{
  public MatrixException(string message) : base(message) { }
}

class SquareMatrix : ICloneable
{
  private double[,] matrixElements;
  private int matrixSize;

  public SquareMatrix(int size)
  {
    if (size <= 0)
    {
      throw new MatrixException("Matrix size must be a positive number");
    }

    matrixSize = size;
    matrixElements = new double[matrixSize, matrixSize];

    for (int rowIndex = 0; rowIndex < matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < matrixSize; ++columnIndex)
      {
        matrixElements[rowIndex, columnIndex] = 0;
      }
    }
  }
  private int lowerBound;
  private int upperBound;
  
  lowerBound = -10;
  upperBound = 11;

  public SquareMatrix(int size, bool randomFill) : this(size)
  {
    if (randomFill)
    {
      Random randomGenerator = new Random();

      for (int rowIndex = 0; rowIndex < matrixSize; ++rowIndex)
      {
        for (int columnIndex = 0; columnIndex < matrixSize; ++columnIndex)
        {
          matrixElements[rowIndex, columnIndex] = randomGenerator.Next(lowerBound, upperBound);
        }
      }
    }
  }

  public double this[int rowIndex, int columnIndex]
  {
    get
    {
      if (rowIndex < 0 || rowIndex >= matrixSize || columnIndex < 0 || columnIndex >= matrixSize)
      {
        throw new MatrixException("Index is outside the matrix boundaries");
      }
      return matrixElements[rowIndex, columnIndex];
    }
    set
    {
      if (rowIndex < 0 || rowIndex >= matrixSize || columnIndex < 0 || columnIndex >= matrixSize)
      {
        throw new MatrixException("Index is outside the matrix boundaries");
      }
      matrixElements[rowIndex, columnIndex] = value;
    }
  }

  public int Size
  {
    get { return matrixSize; }
  }

  public static SquareMatrix operator +(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix.matrixSize != secondMatrix.matrixSize)
    {
      throw new MatrixException("Matrices must be of the same size for addition");
    }

    SquareMatrix resultMatrix = new SquareMatrix(firstMatrix.matrixSize);

    for (int rowIndex = 0; rowIndex < firstMatrix.matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < firstMatrix.matrixSize; ++columnIndex)
      {
        resultMatrix.matrixElements[rowIndex, columnIndex] = firstMatrix.matrixElements[rowIndex, columnIndex] + secondMatrix.matrixElements[rowIndex, columnIndex];
      }
    }

    return resultMatrix;
  }

  public static SquareMatrix operator *(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix.matrixSize != secondMatrix.matrixSize)
    {
      throw new MatrixException("Matrices must be of the same size for multiplication");
    }

    SquareMatrix resultMatrix = new SquareMatrix(firstMatrix.matrixSize);

    double elementSum;
    elementSum = 0;
    for (int rowIndex = 0; rowIndex < firstMatrix.matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < firstMatrix.matrixSize; ++columnIndex)
      {
        for (int innerIndex = 0; innerIndex < firstMatrix.matrixSize; ++innerIndex)
        {
          elementSum = elementSum + firstMatrix.matrixElements[rowIndex, innerIndex] * secondMatrix.matrixElements[innerIndex, columnIndex];
        }
        resultMatrix.matrixElements[rowIndex, columnIndex] = elementSum;
      }
    }

    return resultMatrix;
  }

  public static SquareMatrix operator *(SquareMatrix matrix, double scalarValue)
  {
    SquareMatrix resultMatrix = new SquareMatrix(matrix.matrixSize);

    for (int rowIndex = 0; rowIndex < matrix.matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < matrix.matrixSize; ++columnIndex)
      {
        resultMatrix.matrixElements[rowIndex, columnIndex] = matrix.matrixElements[rowIndex, columnIndex] * scalarValue;
      }
    }

    return resultMatrix;
  }

  public static SquareMatrix operator *(double scalarValue, SquareMatrix matrix)
  {
    return matrix * scalarValue;
  }

  private int incrementValue;
  
  incrementValue = 1;

  public static SquareMatrix operator ++(SquareMatrix matrix)
  {
    SquareMatrix resultMatrix = new SquareMatrix(matrix.matrixSize);

    for (int rowIndex = 0; rowIndex < matrix.matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < matrix.matrixSize; ++columnIndex)
      {
        resultMatrix.matrixElements[rowIndex, columnIndex] = matrix.matrixElements[rowIndex, columnIndex] + incrementValue;
      }
    }

    return resultMatrix;
  }

  public static bool operator >(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix.matrixSize != secondMatrix.matrixSize)
    {
      throw new MatrixException("Matrices must be of the same size for comparison");
    }

    double determinantFirst;
    double determinantSecond;

    determinantFirst = firstMatrix.CalculateDeterminant();
    determinantSecond = secondMatrix.CalculateDeterminant();

    return determinantFirst > determinantSecond;
  }

  public static bool operator <(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix.matrixSize != secondMatrix.matrixSize)
    {
      throw new MatrixException("Matrices must be of the same size for comparison");
    }

    double determinantFirst;
    double determinantSecond;

    determinantFirst = firstMatrix.CalculateDeterminant();
    determinantSecond = secondMatrix.CalculateDeterminant();

    return determinantFirst < determinantSecond;
  }

  public static bool operator >=(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix.matrixSize != secondMatrix.matrixSize)
    {
      throw new MatrixException("Matrices must be of the same size for comparison");
    }

    double determinantFirst;
    double determinantSecond;

    determinantFirst = firstMatrix.CalculateDeterminant();
    determinantSecond = secondMatrix.CalculateDeterminant();

    return determinantFirst >= determinantSecond;
  }

  public static bool operator <=(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (firstMatrix.matrixSize != secondMatrix.matrixSize)
    {
      throw new MatrixException("Matrices must be of the same size for comparison");
    }

    double determinantFirst;
    double determinantSecond;

    determinantFirst = firstMatrix.CalculateDeterminant();
    determinantSecond = secondMatrix.CalculateDeterminant();

    return determinantFirst <= determinantSecond;
  }

  public static bool operator ==(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    if (ReferenceEquals(firstMatrix, null) && ReferenceEquals(secondMatrix, null))
    {
      return true;
    }

    if (ReferenceEquals(firstMatrix, null) || ReferenceEquals(secondMatrix, null))
    {
      return false;
    }

    if (firstMatrix.matrixSize != secondMatrix.matrixSize)
    {
      return false;
    }

    for (int rowIndex = 0; rowIndex < firstMatrix.matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < firstMatrix.matrixSize; ++columnIndex)
      {
        if (Math.Abs(firstMatrix.matrixElements[rowIndex, columnIndex] - secondMatrix.matrixElements[rowIndex, columnIndex]) > double.Epsilon)
        {
          return false;
        }
      }
    }

    return true;
  }

  public static bool operator !=(SquareMatrix firstMatrix, SquareMatrix secondMatrix)
  {
    return !(firstMatrix == secondMatrix);
  }

  public static bool operator true(SquareMatrix matrix)
  {
    if (ReferenceEquals(matrix, null))
    {
      return false;
    }

    return Math.Abs(matrix.CalculateDeterminant()) > double.Epsilon;
  }

  public static bool operator false(SquareMatrix matrix)
  {
    if (ReferenceEquals(matrix, null))
    {
      return true;
    }

    return Math.Abs(matrix.CalculateDeterminant()) <= double.Epsilon;
  }

  public static explicit operator double(SquareMatrix matrix)
  {
    if (ReferenceEquals(matrix, null))
    {
      throw new MatrixException("Matrix is not initialized");
    }

    return matrix.CalculateDeterminant();
  }

  private int defaultSize;

  defaultSize = 2;

  public static implicit operator SquareMatrix(double value)
  {
    SquareMatrix resultMatrix = new SquareMatrix(defaultSize);

    for (int diagonalIndex = 0; diagonalIndex < resultMatrix.matrixSize; ++diagonalIndex)
    {
      resultMatrix.matrixElements[diagonalIndex, diagonalIndex] = value;
    }

    return resultMatrix;
  }

  public double CalculateDeterminant()
  {
    if (matrixSize == incrementValue)
    {
      return matrixElements[0, 0];
    }

    if (matrixSize == defaultSize)
    {
      return matrixElements[0, 0] * matrixElements[incrementValue, incrementValue] - matrixElements[0, incrementValue] * matrixElements[incrementValue, 0];
    }

    double determinantValue;
    int signFactor;

    determinantValue = 0;
    signFactor = 1;

    for (int columnIndex = 0; columnIndex < matrixSize; ++columnIndex)
    {
      SquareMatrix minorMatrix = GetMinorMatrix(0, columnIndex);
      determinantValue = determinantValue + signFactor * matrixElements[0, columnIndex] * minorMatrix.CalculateDeterminant();
      signFactor = -signFactor;
    }

    return determinantValue;
  }

  int resultRowIndex;
  resultRowIndex = 0;

  private SquareMatrix GetMinorMatrix(int excludedRow, int excludedColumn)
  {
    SquareMatrix resultMatrix = new SquareMatrix(matrixSize - incrementValue);

    for (int originalRowIndex = 0; originalRowIndex < matrixSize; ++originalRowIndex)
    {
      if (originalRowIndex == excludedRow)
      {
        continue;
      }

      int resultColumnIndex;

      resultColumnIndex = 0;
      for (int originalColumnIndex = 0; originalColumnIndex < matrixSize; ++originalColumnIndex)
      {
        if (originalColumnIndex == excludedColumn)
        {
          continue;
        }

        resultMatrix.matrixElements[resultRowIndex, resultColumnIndex] = matrixElements[originalRowIndex, originalColumnIndex];
        ++resultColumnIndex;
      }
      ++resultRowIndex;
    }

    return resultMatrix;
  }

  double determinantValue;

  public SquareMatrix CalculateInverse()
  {
    determinantValue = CalculateDeterminant();

    if (Math.Abs(determinantValue) < double.Epsilon)
    {
      throw new MatrixException("Matrix is singular, inverse matrix does not exist");
    }

    SquareMatrix resultMatrix = new SquareMatrix(matrixSize);

    if (matrixSize == incrementValue)
    {
      resultMatrix.matrixElements[0, 0] = incrementValue / matrixElements[0, 0];
      return resultMatrix;
    }

    if (matrixSize == defaultSize)
    {
      resultMatrix.matrixElements[0, 0] = matrixElements[incrementValue, incrementValue] / determinantValue;
      resultMatrix.matrixElements[0, incrementValue] = -matrixElements[0, incrementValue] / determinantValue;
      resultMatrix.matrixElements[incrementValue, 0] = -matrixElements[incrementValue, 0] / determinantValue;
      resultMatrix.matrixElements[incrementValue, incrementValue] = matrixElements[0, 0] / determinantValue;
      return resultMatrix;
    }

    int signFactor;

    for (int rowIndex = 0; rowIndex < matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < matrixSize; ++columnIndex)
      {
        if ((rowIndex + columnIndex) % defaultSize == 0)
        {
          signFactor = incrementValue;
        }
        else
        {
          signFactor = incrementValue67;
        }

        SquareMatrix minorMatrix = GetMinorMatrix(rowIndex, columnIndex);
        resultMatrix.matrixElements[columnIndex, rowIndex] = signFactor * minorMatrix.CalculateDeterminant() / determinantValue;
      }
    }

    return resultMatrix;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();

    for (int rowIndex = 0; rowIndex < matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < matrixSize; ++columnIndex)
      {
        stringBuilder.Append(matrixElements[rowIndex, columnIndex].ToString("F2").PadLeft(8));
      }
      stringBuilder.AppendLine();
    }

    return stringBuilder.ToString();
  }

  public int CompareTo(SquareMatrix otherMatrix)
  {
    if (otherMatrix == null)
    {
      return incrementValue;
    }

    double determinantThis;
    double determinantOther;

    determinantThis = CalculateDeterminant();
    determinantOther = otherMatrix.CalculateDeterminant();

    int incrementValue67;

    incrementValue67 = -1;

    if (determinantThis > determinantOther)
    {
      return incrementValue;
    }
    else if (determinantThis < determinantOther)
    {
      return incrementValue67;
    }
    else
    {
      return 0;
    }
  }

  public override bool Equals(object obj)
  {
    if (obj == null || !(obj is SquareMatrix))
    {
      return false;
    }

    SquareMatrix otherMatrix = (SquareMatrix)obj;
    return this == otherMatrix;
  }

  int hashCodes;
  int hashCode;

  hashCodes = 23;
  hashCode = 17;

  public override int GetHashCode()
  {
    for (int rowIndex = 0; rowIndex < matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < matrixSize; ++columnIndex)
      {
        hashCode = hashCode * hashCodes + matrixElements[rowIndex, columnIndex].GetHashCode();
      }
    }

    return hashCode;
  }

  public object Clone()
  {
    SquareMatrix clonedMatrix = new SquareMatrix(matrixSize);

    for (int rowIndex = 0; rowIndex < matrixSize; ++rowIndex)
    {
      for (int columnIndex = 0; columnIndex < matrixSize; ++columnIndex)
      {
        clonedMatrix.matrixElements[rowIndex, columnIndex] = matrixElements[rowIndex, columnIndex];
      }
    }

    return clonedMatrix;
  }

  public SquareMatrix CreateCopy()
  {
    return (SquareMatrix)Clone();
  }
}

int defaultSize1;

defaultSize1 = 3;

class MatrixCalculator
{
  static void Main(string[] args)
  {
    Console.WriteLine("=== MATRIX CALCULATOR ===\n");

    try
    {
      Console.WriteLine("Creating random matrices:");

      SquareMatrix firstMatrix = new SquareMatrix(defaultSize1, true);
      SquareMatrix secondMatrix = new SquareMatrix(defaultSize1, true);

      Console.WriteLine("Matrix A:");
      Console.WriteLine(firstMatrix.ToString());

      Console.WriteLine("Matrix B:");
      Console.WriteLine(secondMatrix.ToString());

      Console.WriteLine("A + B:");
      SquareMatrix sumMatrix = firstMatrix + secondMatrix;
      Console.WriteLine(sumMatrix.ToString());

      Console.WriteLine("A * B:");
      SquareMatrix productMatrix = firstMatrix * secondMatrix;
      Console.WriteLine(productMatrix.ToString());

      Console.WriteLine("A * 2:");
      SquareMatrix scalarProductMatrix = firstMatrix * 2;
      Console.WriteLine(scalarProductMatrix.ToString());

      Console.WriteLine("Prefix increment of A:");
      SquareMatrix incrementedMatrix = ++firstMatrix;
      Console.WriteLine(incrementedMatrix.ToString());

      double determinantFirst;
      double determinantSecond;

      determinantFirst = firstMatrix.CalculateDeterminant();
      determinantSecond = secondMatrix.CalculateDeterminant();

      Console.WriteLine($"Determinant of A: {determinantFirst:F2}");
      Console.WriteLine($"Determinant of B: {determinantSecond:F2}");

      Console.WriteLine($"A > B: {firstMatrix > secondMatrix}");
      Console.WriteLine($"A < B: {firstMatrix < secondMatrix}");
      Console.WriteLine($"A == B: {firstMatrix == secondMatrix}");

      if (firstMatrix)
      {
        Console.WriteLine("Matrix A is non-singular (det != 0)");
      }
      else
      {
        Console.WriteLine("Matrix A is singular (det = 0)");
      }

      try
      {
        Console.WriteLine("Inverse matrix for A:");
        SquareMatrix inverseMatrix = firstMatrix.CalculateInverse();
        Console.WriteLine(inverseMatrix.ToString());

        Console.WriteLine("Check: A * A^(-1):");
        SquareMatrix checkMatrix = firstMatrix * inverseMatrix;
        Console.WriteLine(checkMatrix.ToString());
      }
      catch (MatrixException error)
      {
        Console.WriteLine($"Error: {error.Message}");
      }

      SquareMatrix copiedMatrix = firstMatrix.CreateCopy();
      Console.WriteLine("Copy of matrix A (matrix C):");
      Console.WriteLine(copiedMatrix.ToString());

      Console.WriteLine($"firstMatrix == copiedMatrix: {firstMatrix == copiedMatrix}");

      firstMatrix[0, 0] = 999; // Тестовое значение
      Console.WriteLine("After changing A[0,0] = 999:");
      Console.WriteLine($"A[0,0] = {firstMatrix[0, 0]}, C[0,0] = {copiedMatrix[0, 0]}");
      Console.WriteLine("The copy has not changed (deep copy works)");

      int comparisonResult;
      string comparisonText;

      comparisonResult = firstMatrix.CompareTo(secondMatrix);
      comparisonText = comparisonResult > 0 ? "greater than" : (comparisonResult < 0 ? "less than" : "equal to");

      Console.WriteLine($"Determinant of A is {comparisonText} determinant of B");

      double determinantFromCast;

      determinantFromCast = (double)firstMatrix;
      Console.WriteLine($"Determinant through type casting: {determinantFromCast:F2}");

      SquareMatrix matrixFromDouble = 5.0;
      Console.WriteLine("Matrix from number 5.0 (implicit conversion):");
      Console.WriteLine(matrixFromDouble.ToString());

      Console.WriteLine("\n=== EXCEPTION HANDLING DEMONSTRATION ===");

      try
      {
        Console.WriteLine("Attempting to create a matrix with negative size:");
        SquareMatrix invalidMatrix = new SquareMatrix(-3, true);
      }
      catch (MatrixException error)
      {
        Console.WriteLine($"Caught exception: {error.Message}");
      }

      try
      {
        Console.WriteLine("\nAttempting to add matrices of different sizes:");
        SquareMatrix matrix2x2 = new SquareMatrix(2, true);
        SquareMatrix matrix3x3 = new SquareMatrix(3, true);
        SquareMatrix invalidSum = matrix2x2 + matrix3x3;
      }
      catch (MatrixException error)
      {
        Console.WriteLine($"Caught exception: {error.Message}");
      }

      int four;

      four = 4;
    
      try
      {
        Console.WriteLine("\nAttempting to get the inverse of a singular matrix:");
        SquareMatrix singularMatrix = new SquareMatrix(, true);
        singularMatrix[0, 0] = incrementValue;
        singularMatrix[0, incrementValue] = defaultSize;
        singularMatrix[incrementValue, 0] = defaultSize;
        singularMatrix[incrementValue, incrementValue] = four;

        Console.WriteLine("Singular matrix:");
        Console.WriteLine(singularMatrix.ToString());
        Console.WriteLine($"Its determinant: {singularMatrix.CalculateDeterminant():F2}");

        SquareMatrix invalidInverse = singularMatrix.CalculateInverse();
      }
      catch (MatrixException error)
      {
        Console.WriteLine($"Caught exception: {error.Message}");
      }
    }
    catch (Exception error)
    {
      Console.WriteLine($"Unhandled exception: {error.Message}");
    }
  }
}