import { Link } from "react-router-dom"

function Articles({articles = []}) {

    return (
        <div>
            {articles?.map(article =>
                <article className={`shadow-sm flex flex-col my-4 space-y-3 border-l-8 border-blue-600`}>
                    <div className="border pl-4 pr-4 py-4">
                        <Link className="no-underline text-2xl font-bold sm:text-4xl" to={article.url}>
                            <h1 className="mb-4">{article.heading}</h1>
                            <p className="w-full text-lg font-medium leading-normal !no-underline">{article.preamble}</p>
                        </Link>
                    </div>
                </article>
            )}
        </div>
    )
}

export default Articles;