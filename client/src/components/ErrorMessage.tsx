import React from "react";

interface ErrorMessageProps {
  message: string;
}

const ErrorMessage: React.FC<ErrorMessageProps> = ({ message }) => {
  return (
    <div>
      <p>Error: {message}</p>
    </div>
  );
};

export default ErrorMessage;
