function Alert({heading, message, type}) {

    const getAlert = () => {
        switch(type) {
            case 'error':
                return <Error heading={heading} message={message}/>
            default:
                return  <Info heading={heading} message={message}/>
        }
    }

    return (
        <div className="fixed top-1/2 left-1/2 -mt-24 -ml-96 z-50">
            {getAlert()}
        </div>
    );
}

function Error({heading, message}) {
    return (
        <div role="alert">
            <div className="bg-red-500 text-white font-bold rounded-t px-4 py-2">
                {heading}
            </div>
            <div className="border border-t-0 border-red-400 rounded-b bg-red-100 px-4 py-3 text-red-700">
                <p>{message}</p>
            </div>
        </div>
    )
}

function Info({heading, message}) {
    return (
        <div role="alert">
            <div className="bg-blue-500 text-white font-bold rounded-t px-4 py-2">
                {heading}
            </div>
            <div className="border border-t-0 border-blue-400 rounded-b bg-blue-100 px-4 py-3 text-blue-700">
                <p>{message}</p>
            </div>
        </div>
    )
}

export default Alert;