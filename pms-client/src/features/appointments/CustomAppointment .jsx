import React from 'react';

function CustomAppointment ({ data, style, children, ...restProps }) {

    const appointmentStyle = {
        backgroundColor: data.appointmentType === 'On Site' ? 'green' : 'blue',
        color: '#fff', 
        ...style
    };


  return (
    <div
    {...restProps}
    style={appointmentStyle}
>
    {children}
</div>
  )
}

export default CustomAppointment 
