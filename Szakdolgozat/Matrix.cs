namespace Szakdolgozat
{
    public class Matrix : IDisposable
    {
        public double[,] ContentsArray { get; set; }
        public int ActualRows { get; set; }
        public int ActualCols { get; set; }
        public PictureBox PanelPicturebox { get; set; }
        public Panel MatrixPanel { get; set; }
        public Panel RowScrollbar { get; set; }
        public Panel ColScrollbar { get; set; }
        public PictureBox RowPicturebox { get; set; }
        public PictureBox ColPicturebox { get; set; }
        public VScrollBar VerticalScrollBar { get; set; }
        public HScrollBar HorizontalScrollBar { get; set; }
        public Matrix(int RowCount, int ColCount, PictureBox PanelPictureboxName, PictureBox RowPicturebox, PictureBox ColPicturebox)
        {
            this.ContentsArray = new double[RowCount, ColCount];
            this.ActualRows = RowCount;
            this.ActualCols = ColCount;
            this.PanelPicturebox = PanelPictureboxName;
            this.RowPicturebox = RowPicturebox;
            this.ColPicturebox = ColPicturebox;

            this.MatrixPanel = (Panel)PanelPictureboxName.Parent;
            this.RowScrollbar = (Panel)RowPicturebox.Parent;
            this.ColScrollbar = (Panel)ColPicturebox.Parent;

            this.VerticalScrollBar = (VScrollBar)RowScrollbar.Tag;
            this.HorizontalScrollBar = (HScrollBar)ColScrollbar.Tag;

            this.VerticalScrollBar.Tag = this;
            this.HorizontalScrollBar.Tag = this;

            this.PanelPicturebox.Tag = this;
            this.MatrixPanel.Tag = this;
            this.RowPicturebox.Tag = this;
            this.ColPicturebox.Tag = this;
        }
        public double[,] ResizeArray(int x, int y)
        {
            double[,] newArray = new double[x, y];
            int minX = Math.Min(x, this.ContentsArray.GetLength(0));
            int minY = Math.Min(y, this.ContentsArray.GetLength(1));
            Parallel.For(0, minY, i =>
            {
                for (int j = 0; j < minX; ++j)
                {
                    newArray[j, i] = this.ContentsArray[j, i];
                }
            });
            this.ActualRows = x;
            this.ActualCols = y;
            return newArray;
        }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}
