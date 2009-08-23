namespace developwithpassion.bdd.core
{
    public interface FieldSwitcher {
        PipelineBehaviour to(object new_value);
    }

    public class FieldSwitcherImplementation : FieldSwitcher
    {
        MemberTarget member_target;
        object original_value;

        public FieldSwitcherImplementation(MemberTarget member_target)
        {
            this.member_target = member_target;
            original_value = member_target.get_value();
        }

        public PipelineBehaviour to(object new_value)
        {
            return new PipelineBehaviour(() => member_target.change_value_to(new_value),
                                         () => member_target.change_value_to(original_value));
        }
    }
}