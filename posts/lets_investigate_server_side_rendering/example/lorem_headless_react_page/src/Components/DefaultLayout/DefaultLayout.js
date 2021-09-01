import { Logo } from "../Logo/Logo";

function DefaultLayout({children}) {
    return (
        <div>
            <header className="pt-2 border-b-2 border-greyLight">
                <div>
                    <div className="flex flex-col  px-2 my-2 space-y-6 mx-auto max-w-5xl">
                        <div className="w-full flex justify-between">
                            <Logo/> 
                        </div>
                    </div>
                </div>
            </header>

            <main className="flex flex-col px-2 my-2 space-y-6 mx-auto max-w-5xl">
                {children}
            </main>
        </div>
    );
}

export default DefaultLayout;