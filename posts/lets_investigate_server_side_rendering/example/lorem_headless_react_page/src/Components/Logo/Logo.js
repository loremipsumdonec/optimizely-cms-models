
export function Logo({name, url}) {

    return (
        <div className="flex-1 flex">
            <a href={url} title="tiff.se: a simple blog" className={`flex text-gray-600 font-medium transition-colors duration-200 hover:text-gray-900`}>
                <span className="align-bottom material-icons-outlined mr-2 pt-1 text-3xl">api</span>
                {name}
            </a>
        </div>
    )
}
