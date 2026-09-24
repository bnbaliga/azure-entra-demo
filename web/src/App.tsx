import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import Header from "./components/Header";
import Landing from "./components/Landing";
import ProfilePanel from "./components/ProfilePanel";
import TodosPanel from "./components/TodosPanel";

export default function App() {
  return (
    <>
      <Header />
      <main className="container">
        <UnauthenticatedTemplate>
          <Landing />
        </UnauthenticatedTemplate>
        <AuthenticatedTemplate>
          <div className="grid">
            <ProfilePanel />
            <TodosPanel />
          </div>
        </AuthenticatedTemplate>
      </main>
    </>
  );
}
