interface ErrorDisplayProps {
  error: unknown;
  entityName: string;
}

export function ErrorDisplay({ error, entityName }: ErrorDisplayProps) {
  const getErrorMessage = () => {
    if (error instanceof Error) {
      return error.message;
    }
    if (typeof error === 'object' && error !== null) {
      const err = error as any;
      if (err.response?.data?.error) {
        return err.response.data.error;
      }
      if (err.response?.data) {
        return JSON.stringify(err.response.data);
      }
      if (err.message) {
        return err.message;
      }
      if (err.request) {
        return 'Network error: No response from server. Check CORS settings and ensure backend is running.';
      }
    }
    return `Failed to load ${entityName}. Make sure the backend is running on https://localhost:7169`;
  };

  const errorMessage = getErrorMessage();
  
  console.error('Full error object:', error);

  return (
    <div className="error">
      <strong>Error loading {entityName}:</strong>
      <p>{errorMessage}</p>
      <p style={{ fontSize: '0.9em', color: '#666', marginTop: '10px' }}>
        Please check:
        <br />• Is the backend running?
        <br />• Open <a href="https://localhost:7169/api/HealthApi" target="_blank" rel="noopener noreferrer">https://localhost:7169/api/HealthApi</a> in browser
        <br />• Check browser console (F12) for details
        <br />• Check Network tab in DevTools - look for CORS errors
        <br />• Verify the request URL matches the backend port
      </p>
    </div>
  );
}
