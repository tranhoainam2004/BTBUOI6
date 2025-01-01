using Lab04_01.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lab04_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvStudent.CellClick += dgvStudent_CellClick; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (StudenContextDB context = new StudenContextDB())
                {
                    List<FACULTY> listFacultys = context.FACULTies.ToList();
                    List<STUDENT> listStudent = context.STUDENTs.ToList();
                    FillFacultyCombobox(listFacultys);
                    BindGrid(listStudent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillFacultyCombobox(List<FACULTY> listFacultys)
        {
            this.cmbFaculty.DataSource = listFacultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyId";
        }

        private void BindGrid(List<STUDENT> listStudent)
        {
            dgvStudent.Rows.Clear();
            dgvStudent.Columns.Clear();


            dgvStudent.Columns.Add("StudentID", "MSSV");
            dgvStudent.Columns.Add("FullName", "Họ và Tên");
            dgvStudent.Columns.Add("FacultyName", "Khoa");
            dgvStudent.Columns.Add("AverageScore", "Điểm");
            dgvStudent.Columns.Add("FacultyID", "FacultyID"); 
            dgvStudent.Columns["FacultyID"].Visible = false; 

            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                dgvStudent.Rows[index].Cells[2].Value = item.FACULTY.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore;
                dgvStudent.Rows[index].Cells[4].Value = item.FacultyID;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiemTb.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin: Mã SV, Họ tên và Điểm TB!");
                return;
            }

            try
            {
                using (StudenContextDB context = new StudenContextDB())
                {
                    var newStudent = new STUDENT
                    {
                        StudentID = txtMaSV.Text,
                        FullName = txtHoTen.Text,
                        FacultyID = (int)cmbFaculty.SelectedValue,
                        AverageScore = float.Parse(txtDiemTb.Text)
                    };

                    context.STUDENTs.Add(newStudent);
                    context.SaveChanges();
                    MessageBox.Show("Thêm sinh viên thành công!");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiemTb.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin: Mã SV, Họ tên và Điểm TB!");
                return;
            }

            try
            {
                using (StudenContextDB context = new StudenContextDB())
                {
                    string studentID = txtMaSV.Text;
                    var studentToUpdate = context.STUDENTs.FirstOrDefault(s => s.StudentID == studentID);

                    if (studentToUpdate != null)
                    {
                        studentToUpdate.FullName = txtHoTen.Text;
                        studentToUpdate.FacultyID = (int)cmbFaculty.SelectedValue;
                        studentToUpdate.AverageScore = float.Parse(txtDiemTb.Text);

                        context.SaveChanges();
                        MessageBox.Show("Sửa thông tin sinh viên thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên để sửa!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa thông tin sinh viên: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                using (StudenContextDB context = new StudenContextDB())
                {
                    string studentID = txtMaSV.Text;
                    var studentToDelete = context.STUDENTs.FirstOrDefault(s => s.StudentID == studentID);

                    if (studentToDelete != null)
                    {
                        context.STUDENTs.Remove(studentToDelete);
                        context.SaveChanges();
                        MessageBox.Show("Xóa sinh viên thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên để xóa!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message);
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                var selectedRow = dgvStudent.Rows[e.RowIndex];

                
                txtMaSV.Text = selectedRow.Cells["StudentID"].Value?.ToString() ?? string.Empty;
                txtHoTen.Text = selectedRow.Cells["FullName"].Value?.ToString() ?? string.Empty;
                txtDiemTb.Text = selectedRow.Cells["AverageScore"].Value?.ToString() ?? string.Empty;
                int facultyIndex = selectedRow.Cells["FacultyID"].Value != null ? (int)selectedRow.Cells["FacultyID"].Value : -1;
                cmbFaculty.SelectedValue = facultyIndex;
            }
        }
    }
}