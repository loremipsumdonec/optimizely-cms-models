import DefaultLayout from "../DefaultLayout/DefaultLayout";
import { Link } from "react-router-dom"

function StartPage({heading, preamble, articles}) {
    return (
        <DefaultLayout>
            <h1 className="text-5xl mb-4 font-semibold">{heading}</h1>
            <p className="text-2xl font-medium mb-10">{preamble}</p>

            <div>
                {articles.map(article =>
                    <article className={`shadow-sm flex flex-col my-4 space-y-3 border-l-8 border-blue-600`}>
                        <div className="border pl-4 pr-4 py-4">
                            <Link className="no-underline text-2xl font-bold sm:text-4xl" to={article.url}>
                                <h1 className="mb-4">{article.heading}</h1>
                                <p className="w-full text-lg font-medium leading-normal !no-underline">{preamble}</p>
                            </Link>
                        </div>
                    </article>
                )}
            </div>

        </DefaultLayout>
    );
}

export default StartPage;