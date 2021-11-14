window.ShowToastr = (type, message) => {
    toastr.options ={
        "positionClass": "toast-bottom-right"
    }
    if(type === 'success'){
        toastr.success(message, 'Operation successful')
    }

    if(type === 'error'){
        toastr.error(message, 'Operation failed')
    }
}