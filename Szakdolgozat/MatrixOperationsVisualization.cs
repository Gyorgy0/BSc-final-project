namespace Szakdolgozat
{
    public partial class Form1 : Form
    {
        // These methods use MatrixQueryVisualization Visualizationform
        Matrix VisualizedMatrix;
        private void MaxSearchVisualization(object? sender, EventArgs e)
        {
            MatrixQueryVisualization MatrixQueryVisualizator = new MatrixQueryVisualization(VisualizedMatrix, StepCounter);
            MatrixQueryVisualizator.Text = "Maximum keresése...";
            MatrixQueryVisualizator.ActualOperation = MatrixQueryVisualizator.MaxSearchStep;
            button1.Enabled = false;
            MatrixQueryVisualizator.Show();
        }

        private void MinSearchVisualization(object? sender, EventArgs e)
        {
            MatrixQueryVisualization MatrixQueryVisualizator = new MatrixQueryVisualization(VisualizedMatrix, StepCounter);
            MatrixQueryVisualizator.Text = "Minimum keresése...";
            MatrixQueryVisualizator.ActualOperation = MatrixQueryVisualizator.MinSearchStep;
            button1.Enabled = false;
            MatrixQueryVisualizator.Show();
        }

        private void OddCounterVisualization(object? sender, EventArgs e)
        {
            MatrixQueryVisualization MatrixQueryVisualizator = new MatrixQueryVisualization(VisualizedMatrix, StepCounter);
            MatrixQueryVisualizator.Text = "Páratlan számok megszámlálása...";
            MatrixQueryVisualizator.ActualOperation = MatrixQueryVisualizator.OddCounterStep;
            button1.Enabled = false;
            MatrixQueryVisualizator.Show();
        }

        private void EvenCounterVisualization(object? sender, EventArgs e)
        {
            MatrixQueryVisualization MatrixQueryVisualizator = new MatrixQueryVisualization(VisualizedMatrix, StepCounter);
            MatrixQueryVisualizator.Text = "Páros számok megszámlálása...";
            MatrixQueryVisualizator.ActualOperation = MatrixQueryVisualizator.EvenCounterStep;
            button1.Enabled = false;
            MatrixQueryVisualizator.Show();
        }

        private void CustomNumberSearchVisualization(object? sender, EventArgs e)
        {
            MatrixQueryVisualization MatrixQueryVisualizator = new MatrixQueryVisualization(VisualizedMatrix, StepCounter);
            MatrixQueryVisualizator.Text = "Adott szám megszámlálása...";
            MatrixQueryVisualizator.SearchValue = ProvidedNumber;
            MatrixQueryVisualizator.ActualOperation = MatrixQueryVisualizator.CustomNumberSearchStep;
            button1.Enabled = false;
            MatrixQueryVisualizator.Show();
        }

        // These methods use SingleMatrixVisualization VisualizationForm
        private void TransposeVisualization(object? sender, EventArgs e)
        {
            SingleMatrixVisualization singleMatrixVisualizator = new SingleMatrixVisualization(VisualizedMatrix, StepCounter);
            singleMatrixVisualizator.Text = "Mátrix transzponálása...";
            singleMatrixVisualizator.ActualOperation = singleMatrixVisualizator.TransposeStep;
            button1.Enabled = false;
            singleMatrixVisualizator.Show();
        }
        private void HorizontalMirroringVisualization(object? sender, EventArgs e)
        {
            SingleMatrixVisualization singleMatrixVisualizator = new SingleMatrixVisualization(VisualizedMatrix, StepCounter);
            singleMatrixVisualizator.Text = "Mátrix vízszintes tükrözése...";
            singleMatrixVisualizator.ActualOperation = singleMatrixVisualizator.HorizontalMirroringStep;
            button1.Enabled = false;
            singleMatrixVisualizator.Show();
        }
        private void VerticalMirroringVisualization(object? sender, EventArgs e)
        {
            SingleMatrixVisualization singleMatrixVisualizator = new SingleMatrixVisualization(VisualizedMatrix, StepCounter);
            singleMatrixVisualizator.Text = "Mátrix függőleges tükrözése...";
            singleMatrixVisualizator.ActualOperation = singleMatrixVisualizator.VerticalMirroringStep;
            button1.Enabled = false;
            singleMatrixVisualizator.Show();
        }

        private void MatrixRowSortingVisualization(object? sender, EventArgs e)
        {
            SingleMatrixVisualization singleMatrixVisualizator = new SingleMatrixVisualization(VisualizedMatrix, StepCounter);
            singleMatrixVisualizator.Text = "Mátrix sorainak növekvő sorrend szerinti rendezése...";
            singleMatrixVisualizator.ActualOperation = singleMatrixVisualizator.RowSortingStep;
            button1.Enabled = false;
            singleMatrixVisualizator.Show();
        }

        private void MatrixCollumnSortingVisualization(object? sender, EventArgs e)
        {
            SingleMatrixVisualization singleMatrixVisualizator = new SingleMatrixVisualization(VisualizedMatrix, StepCounter);
            singleMatrixVisualizator.Text = "Mátrix oszlopainak növekvő sorrend szerinti rendezése...";
            singleMatrixVisualizator.ActualOperation = singleMatrixVisualizator.ColSortingStep;
            singleMatrixVisualizator.VerticalPainting = true;
            button1.Enabled = false;
            singleMatrixVisualizator.ShowDialog();
        }

        private void MatrixReverseRowSortingVisualization(object? sender, EventArgs e)
        {
            SingleMatrixVisualization singleMatrixVisualizator = new SingleMatrixVisualization(VisualizedMatrix, StepCounter);
            singleMatrixVisualizator.Text = "Mátrix sorainak csökkenő sorrend szerinti rendezése...";
            singleMatrixVisualizator.ActualOperation = singleMatrixVisualizator.ReverseRowSortingStep;
            button1.Enabled = false;
            singleMatrixVisualizator.Show();
        }

        private void MatrixReverseCollumnSortingVisualization(object? sender, EventArgs e)
        {
            SingleMatrixVisualization singleMatrixVisualizator = new SingleMatrixVisualization(VisualizedMatrix, StepCounter);
            singleMatrixVisualizator.Text = "Mátrix oszlopainak csökkenő sorrend szerinti rendezése...";
            singleMatrixVisualizator.ActualOperation = singleMatrixVisualizator.ReverseColSortingStep;
            singleMatrixVisualizator.VerticalPainting = true;
            button1.Enabled = false;
            singleMatrixVisualizator.Show();
        }
        private void MatrixMultiplicationByNumber(object? sender, EventArgs e)
        {
            SingleMatrixVisualization singleMatrixVisualizator = new SingleMatrixVisualization(VisualizedMatrix, StepCounter);
            singleMatrixVisualizator.Text = "Mátrix szorzása számmal...";
            singleMatrixVisualizator.Multiplicator = this.Multiplicator;
            singleMatrixVisualizator.ActualOperation = singleMatrixVisualizator.MultiplicationByNumberStep;
            button1.Enabled = false;
            singleMatrixVisualizator.Show();
        }
        // These methods use DualMatrixVisualization VisualizationForm
        private void MatrixAddition(object? sender, EventArgs e)
        {
            DualMatrixVisualization dualMatrixVisualizator = new DualMatrixVisualization(matrix1, matrix2, StepCounter);
            dualMatrixVisualizator.Text = "Mátrixok összeadása...";
            dualMatrixVisualizator.ActualOperation = dualMatrixVisualizator.AdditionStep;
            button1.Enabled = false;
            dualMatrixVisualizator.Show();
        }
        private void MatrixMultiplicationByMatrix(object? sender, EventArgs e)
        {
            DualMatrixVisualization dualMatrixVisualizator = new DualMatrixVisualization(matrix1, matrix2, StepCounter);
            dualMatrixVisualizator.Text = "Mátrixok összeszorzása...";
            dualMatrixVisualizator.ActualOperation = dualMatrixVisualizator.MultiplicationByMatrixStep;
            button1.Enabled = false;
            dualMatrixVisualizator.Show();
        }
    }
}
