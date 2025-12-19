import Swal from 'sweetalert2';

export const confirmDelete = async (title: string, text: string = 'This action cannot be undone!'): Promise<boolean> => {
  const result = await Swal.fire({
    title,
    text,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#e74c3c',
    cancelButtonColor: '#95a5a6',
    confirmButtonText: 'Yes, delete it!',
    cancelButtonText: 'Cancel',
  });
  return result.isConfirmed;
};

export const showSuccess = (title: string, text: string = '') => {
  Swal.fire({
    icon: 'success',
    title,
    text,
    timer: 2000,
    showConfirmButton: false,
  });
};

export const showError = (title: string, text: string = '') => {
  Swal.fire({
    icon: 'error',
    title,
    text,
    confirmButtonColor: '#e74c3c',
  });
};

export const showInfo = (title: string, text: string = '') => {
  Swal.fire({
    icon: 'info',
    title,
    text,
  });
};
