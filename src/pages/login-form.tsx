import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import {
  Field,
  FieldDescription,
  FieldGroup,
  FieldLabel,
} from "@/components/ui/field"
import { Input } from "@/components/ui/input"
import { callLogin } from "@/lib/api"

export function LoginForm({
  className,
  ...props
}: React.ComponentProps<"div">) {

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const emailOrUsername = formData.get("email") as string;
    const password = formData.get("password") as string;
    // Call the login API with emailOrUsername and password
    if (emailOrUsername && password) {
      await callLogin(emailOrUsername, password)
        .then((response) => {
          // Handle successful login, e.g., store token, redirect, etc.
          console.log("Login successful:", response.data);
        })
        .catch((error) => {
          // Handle login error, e.g., show error message
          console.error("Login failed:", error);
        });
    } else {
      console.error("Email/Username and password are required.");
    }
  };

  return (
    <div className="flex items-center justify-center h-screen w-full">
      <div className={cn("flex flex-col gap-6 w-100 h-100", className)} {...props}>
        <Card >
          <CardHeader>
            <CardTitle>Login to your account</CardTitle>
            <CardDescription>
              Enter your username or email below to login to your account
            </CardDescription>
          </CardHeader>
          <CardContent>
            <form onSubmit={handleSubmit}>
              <FieldGroup>
                <Field>
                  <FieldLabel htmlFor="email">UserName Or Email</FieldLabel>
                  <Input
                    id="email"
                    name="email"
                    type="text"
                    placeholder="m@example.com or username"
                    required
                  />
                </Field>
                <Field>
                  <div className="flex items-center">
                    <FieldLabel htmlFor="password">Password</FieldLabel>
                    <a
                      href="#"
                      className="ml-auto inline-block text-sm underline-offset-4 hover:underline"
                    >
                      Forgot your password?
                    </a>
                  </div>
                  <Input id="password" name="password" type="password"  required />
                </Field>
                <Field>
                  <Button type="submit">Login</Button>
                  <Button variant="outline" type="button">
                    Login with Google
                  </Button>
                  <FieldDescription className="text-center">
                    Don&apos;t have an account? <a href="/register">Sign up</a>
                  </FieldDescription>
                </Field>
              </FieldGroup>
            </form>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}
